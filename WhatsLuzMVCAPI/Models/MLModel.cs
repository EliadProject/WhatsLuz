﻿using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using LibSVMsharp;
using LibSVMsharp.Helpers;
using Accord.IO;
using Accord.Math;
using Accord.MachineLearning;
using Accord.Controls;
using Accord.MachineLearning.Bayes;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics;
using System.Reflection;
using System.Diagnostics;
using Accord.MachineLearning.VectorMachines.Learning;

namespace WhatsLuzMVCAPI.Models
{
    public class MLModel
    {
        public static Hashtable usersTraining;
       
        public static void Start()
        {
            usersTraining = new Hashtable();
            getAllUsersEvents();
        }

        public static void getAllUsersEvents()
        {
            var db = new SqlConnectionDataContext();

            List<UserAccount> users = db.UserAccounts.ToList(); 
            for (int i =0; i<users.Count(); i++)
            {
                var query = (from sevents in db.SportEvents
                             join uevents in db.Users_Events on sevents.EventID equals uevents.EventID into ueventGroup
                             select new
                             {
                                 category = sevents.CategoryID,
                                 difftime = DateTime.Today.Subtract(sevents.Date).TotalMinutes,
                                 classification = (ueventGroup.Any(x => x.UserID.Equals(users[i].UserID))) ? 1 : 0

                             });
                DataTable userData = CustomLINQtoDataSetMethods.CopyToDataTable(query);
                Train(users[i].UserID, userData );            
            }
            
            
           
        }
        
        public static void Train(int userID, DataTable userData)
        {

           // Convert the DataTable to input and output vectors
            double[][] inputs = userData.ToJagged<double>("category", "difftime");
            int[] outputs = userData.Columns["classification"].ToArray<int>();


            // In our problem, we have 2 classes (samples can be either
            // positive or negative), and 2 inputs (x and y coordinates).

            // Create a Naive Bayes learning algorithm
            var teacher = new LinearCoordinateDescent();

            // Use the learning algorithm to learn
            
            try
            {
                var nb = teacher.Learn(inputs, outputs);
                usersTraining.Add(userID, nb);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);


            }
            /*
            
              // At this point, the learning algorithm should have
              // figured important details about the problem itself:
              int numberOfClasses = nb.NumberOfClasses; // should be 2 (positive or negative)
              int nunmberOfInputs = nb.NumberOfInputs;  // should be 2 (x and y coordinates)
              double[][] test = new double[][]
  {
      new double[] {0,1}
  };



              // Classify the samples using the model
              int[] answers = nb.Decide(test);
  */
            /*
            SVMProblem problem = SVMProblemHelper.Load(@"dataset_path.txt");
            SVMProblem testProblem = SVMProblemHelper.Load(@"test_dataset_path.txt");

            SVMParameter parameter = new SVMParameter();
            parameter.Type = SVMType.C_SVC;
            parameter.Kernel = SVMKernelType.RBF;
            parameter.C = 1;
            parameter.Gamma = 1;

            SVMModel model = SVM.Train(problem, parameter);

            double []target = new double[testProblem.Length];
            for (int i = 0; i < testProblem.Length; i++)
                target[i] = SVM.Predict(model, testProblem.X[i]);

            double accuracy = SVMHelper.EvaluateClassificationProblem(testProblem, target);
            */
        }

        public static void Predict(int userID, SportEvent sevent)
        {
            //Building vector from data
            double[][] test = new double[][]
            {
                new double[] {sevent.CategoryID, DateTime.Today.Subtract(sevent.Date).TotalMinutes }
            };
            foreach (DictionaryEntry s in usersTraining)
            {
                if(!s.Key.Equals(userID))
                {
                   Accord.MachineLearning.VectorMachines.SupportVectorMachine model = s.Value as Accord.MachineLearning.VectorMachines.SupportVectorMachine;
                   bool[] answers = model.Decide(test);
                   Debug.WriteLine("User" + s.Key + ". This event is suitable for him: " + answers[0]);
                }
            }
            
        }

      
    }
    public static class CustomLINQtoDataSetMethods
    {
        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source)
        {
            return new ObjectShredder<T>().Shred(source, null, null);
        }

        public static DataTable CopyToDataTable<T>(this IEnumerable<T> source,
                                                    DataTable table, LoadOption? options)
        {
            return new ObjectShredder<T>().Shred(source, table, options);
        }

    }

    public class ObjectShredder<T>
    {
        private System.Reflection.FieldInfo[] _fi;
        private System.Reflection.PropertyInfo[] _pi;
        private System.Collections.Generic.Dictionary<string, int> _ordinalMap;
        private System.Type _type;

        // ObjectShredder constructor.
        public ObjectShredder()
        {
            _type = typeof(T);
            _fi = _type.GetFields();
            _pi = _type.GetProperties();
            _ordinalMap = new Dictionary<string, int>();
        }

        /// <summary>
        /// Loads a DataTable from a sequence of objects.
        /// </summary>
        /// <param name="source">The sequence of objects to load into the DataTable.</param>
        /// <param name="table">The input table. The schema of the table must match that 
        /// the type T.  If the table is null, a new table is created with a schema 
        /// created from the public properties and fields of the type T.</param>
        /// <param name="options">Specifies how values from the source sequence will be applied to 
        /// existing rows in the table.</param>
        /// <returns>A DataTable created from the source sequence.</returns>
        public DataTable Shred(IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            // Load the table from the scalar sequence if T is a primitive type.
            if (typeof(T).IsPrimitive)
            {
                return ShredPrimitive(source, table, options);
            }

            // Create a new table if the input table is null.
            if (table == null)
            {
                table = new DataTable(typeof(T).Name);
            }

            // Initialize the ordinal map and extend the table schema based on type T.
            table = ExtendTable(table, typeof(T));

            // Enumerate the source sequence and load the object values into rows.
            table.BeginLoadData();
            using (IEnumerator<T> e = source.GetEnumerator())
            {
                while (e.MoveNext())
                {
                    if (options != null)
                    {
                        table.LoadDataRow(ShredObject(table, e.Current), (LoadOption)options);
                    }
                    else
                    {
                        table.LoadDataRow(ShredObject(table, e.Current), true);
                    }
                }
            }
            table.EndLoadData();

            // Return the table.
            return table;
        }

        public DataTable ShredPrimitive(IEnumerable<T> source, DataTable table, LoadOption? options)
        {
            // Create a new table if the input table is null.
            if (table == null)
            {
                table = new DataTable(typeof(T).Name);
            }

            if (!table.Columns.Contains("Value"))
            {
                table.Columns.Add("Value", typeof(T));
            }

            // Enumerate the source sequence and load the scalar values into rows.
            table.BeginLoadData();
            using (IEnumerator<T> e = source.GetEnumerator())
            {
                Object[] values = new object[table.Columns.Count];
                while (e.MoveNext())
                {
                    values[table.Columns["Value"].Ordinal] = e.Current;

                    if (options != null)
                    {
                        table.LoadDataRow(values, (LoadOption)options);
                    }
                    else
                    {
                        table.LoadDataRow(values, true);
                    }
                }
            }
            table.EndLoadData();

            // Return the table.
            return table;
        }

        public object[] ShredObject(DataTable table, T instance)
        {

            FieldInfo[] fi = _fi;
            PropertyInfo[] pi = _pi;

            if (instance.GetType() != typeof(T))
            {
                // If the instance is derived from T, extend the table schema
                // and get the properties and fields.
                ExtendTable(table, instance.GetType());
                fi = instance.GetType().GetFields();
                pi = instance.GetType().GetProperties();
            }

            // Add the property and field values of the instance to an array.
            Object[] values = new object[table.Columns.Count];
            foreach (FieldInfo f in fi)
            {
                values[_ordinalMap[f.Name]] = f.GetValue(instance);
            }

            foreach (PropertyInfo p in pi)
            {
                values[_ordinalMap[p.Name]] = p.GetValue(instance, null);
            }

            // Return the property and field values of the instance.
            return values;
        }

        public DataTable ExtendTable(DataTable table, Type type)
        {
            // Extend the table schema if the input table was null or if the value 
            // in the sequence is derived from type T.            
            foreach (FieldInfo f in type.GetFields())
            {
                if (!_ordinalMap.ContainsKey(f.Name))
                {
                    // Add the field as a column in the table if it doesn't exist
                    // already.
                    DataColumn dc = table.Columns.Contains(f.Name) ? table.Columns[f.Name]
                        : table.Columns.Add(f.Name, f.FieldType);

                    // Add the field to the ordinal map.
                    _ordinalMap.Add(f.Name, dc.Ordinal);
                }
            }
            foreach (PropertyInfo p in type.GetProperties())
            {
                if (!_ordinalMap.ContainsKey(p.Name))
                {
                    // Add the property as a column in the table if it doesn't exist
                    // already.
                    DataColumn dc = table.Columns.Contains(p.Name) ? table.Columns[p.Name]
                        : table.Columns.Add(p.Name, p.PropertyType);

                    // Add the property to the ordinal map.
                    _ordinalMap.Add(p.Name, dc.Ordinal);
                }
            }

            // Return the table.
            return table;
        }
    }
}
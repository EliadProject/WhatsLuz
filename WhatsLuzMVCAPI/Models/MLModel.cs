using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using LibSVMsharp;
using LibSVMsharp.Helpers;
using Accord.Controls;
using Accord.IO;
using Accord.Math;
using Accord.Statistics.Distributions.Univariate;
using Accord.MachineLearning.Bayes;
using System.Data;

namespace WhatsLuzMVCAPI.Models
{
    public class MLModel
    {
        //public static List usersTrainingClassification;
        public static void Start()
        {
            getAllUsersEvents();
        }
        public static void getAllUsersEvents()
        {
            var db = new SqlConnectionDataContext();

            List<UserAccount> users = db.UserAccounts.ToList();
            Hashtable MLData = new Hashtable();
            for (int i =0; i<users.Count(); i++)
            {
                List<EventML> sportsEvents = (from uevents in db.Users_Events
                                              join sevents in db.SportEvents on uevents.EventID equals sevents.EventID

                                              where uevents.UserID == users[i].UserID
                                              select new EventML()
                                              {
                                                  category = sevents.CategoryID,
                                                  difftime = DateTime.Today.Subtract(sevents.Date).TotalMinutes,
                                                  classification = 1
                                                  
                                              }).ToList();
                MLData.Add(users[i].UserID, sportsEvents);
            }

           
        }
        
        public static void Train()
        {
            /*
            // Read the Excel worksheet into a DataTable
            DataTable table = new ExcelReader("examples.xls").GetWorksheet("Classification - Yin Yang");

            // Convert the DataTable to input and output vectors
            double[][] inputs = table.ToJagged<double>("X", "Y");
            int[] outputs = table.Columns["G"].ToArray<int>();

            // Plot the data
            ScatterplotBox.Show("Yin-Yang", inputs, outputs).Hold();
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

        public static void Predict(int model)
        {

        }
    }
}
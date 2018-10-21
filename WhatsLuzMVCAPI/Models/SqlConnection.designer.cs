﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WhatsLuzMVCAPI.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="WhatsLuz")]
	public partial class SqlConnectionDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertUserAccount(UserAccount instance);
    partial void UpdateUserAccount(UserAccount instance);
    partial void DeleteUserAccount(UserAccount instance);
    partial void InsertCategory(Category instance);
    partial void UpdateCategory(Category instance);
    partial void DeleteCategory(Category instance);
    partial void InsertPlace(Place instance);
    partial void UpdatePlace(Place instance);
    partial void DeletePlace(Place instance);
    partial void InsertSportEvent(SportEvent instance);
    partial void UpdateSportEvent(SportEvent instance);
    partial void DeleteSportEvent(SportEvent instance);
    partial void InsertUsers_Event(Users_Event instance);
    partial void UpdateUsers_Event(Users_Event instance);
    partial void DeleteUsers_Event(Users_Event instance);
    #endregion
		
		public SqlConnectionDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["WhatsLuzConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SqlConnectionDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SqlConnectionDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SqlConnectionDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SqlConnectionDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<UserAccount> UserAccounts
		{
			get
			{
				return this.GetTable<UserAccount>();
			}
		}
		
		public System.Data.Linq.Table<Category> Categories
		{
			get
			{
				return this.GetTable<Category>();
			}
		}
		
		public System.Data.Linq.Table<Place> Places
		{
			get
			{
				return this.GetTable<Place>();
			}
		}
		
		public System.Data.Linq.Table<SportEvent> SportEvents
		{
			get
			{
				return this.GetTable<SportEvent>();
			}
		}
		
		public System.Data.Linq.Table<Users_Event> Users_Events
		{
			get
			{
				return this.GetTable<Users_Event>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.UserAccounts")]
	public partial class UserAccount : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _UserID;
		
		private string _Hash;
		
		private string _FacebookID;
		
		private string _DisplayName;
		
		private string _Email;
		
		private System.Nullable<System.DateTime> _LastLogon;
		
		private string _Address;
		
		private string _PhotoURL;
		
		private byte _isAdmin;
		
		private string _accessToken;
		
		private EntitySet<SportEvent> _SportEvents;
		
		private EntitySet<Users_Event> _Users_Events;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnUserIDChanging(int value);
    partial void OnUserIDChanged();
    partial void OnHashChanging(string value);
    partial void OnHashChanged();
    partial void OnFacebookIDChanging(string value);
    partial void OnFacebookIDChanged();
    partial void OnDisplayNameChanging(string value);
    partial void OnDisplayNameChanged();
    partial void OnEmailChanging(string value);
    partial void OnEmailChanged();
    partial void OnLastLogonChanging(System.Nullable<System.DateTime> value);
    partial void OnLastLogonChanged();
    partial void OnAddressChanging(string value);
    partial void OnAddressChanged();
    partial void OnPhotoURLChanging(string value);
    partial void OnPhotoURLChanged();
    partial void OnisAdminChanging(byte value);
    partial void OnisAdminChanged();
    partial void OnaccessTokenChanging(string value);
    partial void OnaccessTokenChanged();
    #endregion
		
		public UserAccount()
		{
			this._SportEvents = new EntitySet<SportEvent>(new Action<SportEvent>(this.attach_SportEvents), new Action<SportEvent>(this.detach_SportEvents));
			this._Users_Events = new EntitySet<Users_Event>(new Action<Users_Event>(this.attach_Users_Events), new Action<Users_Event>(this.detach_Users_Events));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Hash", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Hash
		{
			get
			{
				return this._Hash;
			}
			set
			{
				if ((this._Hash != value))
				{
					this.OnHashChanging(value);
					this.SendPropertyChanging();
					this._Hash = value;
					this.SendPropertyChanged("Hash");
					this.OnHashChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FacebookID", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string FacebookID
		{
			get
			{
				return this._FacebookID;
			}
			set
			{
				if ((this._FacebookID != value))
				{
					this.OnFacebookIDChanging(value);
					this.SendPropertyChanging();
					this._FacebookID = value;
					this.SendPropertyChanged("FacebookID");
					this.OnFacebookIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DisplayName", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string DisplayName
		{
			get
			{
				return this._DisplayName;
			}
			set
			{
				if ((this._DisplayName != value))
				{
					this.OnDisplayNameChanging(value);
					this.SendPropertyChanging();
					this._DisplayName = value;
					this.SendPropertyChanged("DisplayName");
					this.OnDisplayNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this.OnEmailChanging(value);
					this.SendPropertyChanging();
					this._Email = value;
					this.SendPropertyChanged("Email");
					this.OnEmailChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastLogon", DbType="Date")]
		public System.Nullable<System.DateTime> LastLogon
		{
			get
			{
				return this._LastLogon;
			}
			set
			{
				if ((this._LastLogon != value))
				{
					this.OnLastLogonChanging(value);
					this.SendPropertyChanging();
					this._LastLogon = value;
					this.SendPropertyChanged("LastLogon");
					this.OnLastLogonChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Address", DbType="NVarChar(50)")]
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if ((this._Address != value))
				{
					this.OnAddressChanging(value);
					this.SendPropertyChanging();
					this._Address = value;
					this.SendPropertyChanged("Address");
					this.OnAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PhotoURL", DbType="NVarChar(MAX)")]
		public string PhotoURL
		{
			get
			{
				return this._PhotoURL;
			}
			set
			{
				if ((this._PhotoURL != value))
				{
					this.OnPhotoURLChanging(value);
					this.SendPropertyChanging();
					this._PhotoURL = value;
					this.SendPropertyChanged("PhotoURL");
					this.OnPhotoURLChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_isAdmin", DbType="TinyInt NOT NULL")]
		public byte isAdmin
		{
			get
			{
				return this._isAdmin;
			}
			set
			{
				if ((this._isAdmin != value))
				{
					this.OnisAdminChanging(value);
					this.SendPropertyChanging();
					this._isAdmin = value;
					this.SendPropertyChanged("isAdmin");
					this.OnisAdminChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_accessToken", DbType="NVarChar(MAX)")]
		public string accessToken
		{
			get
			{
				return this._accessToken;
			}
			set
			{
				if ((this._accessToken != value))
				{
					this.OnaccessTokenChanging(value);
					this.SendPropertyChanging();
					this._accessToken = value;
					this.SendPropertyChanged("accessToken");
					this.OnaccessTokenChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="UserAccount_SportEvent", Storage="_SportEvents", ThisKey="UserID", OtherKey="OwnerID")]
		public EntitySet<SportEvent> SportEvents
		{
			get
			{
				return this._SportEvents;
			}
			set
			{
				this._SportEvents.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="UserAccount_Users_Event", Storage="_Users_Events", ThisKey="UserID", OtherKey="UserID")]
		public EntitySet<Users_Event> Users_Events
		{
			get
			{
				return this._Users_Events;
			}
			set
			{
				this._Users_Events.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_SportEvents(SportEvent entity)
		{
			this.SendPropertyChanging();
			entity.UserAccount = this;
		}
		
		private void detach_SportEvents(SportEvent entity)
		{
			this.SendPropertyChanging();
			entity.UserAccount = null;
		}
		
		private void attach_Users_Events(Users_Event entity)
		{
			this.SendPropertyChanging();
			entity.UserAccount = this;
		}
		
		private void detach_Users_Events(Users_Event entity)
		{
			this.SendPropertyChanging();
			entity.UserAccount = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Categories")]
	public partial class Category : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _CategoryID;
		
		private string _Name;
		
		private string _Color;
		
		private EntitySet<Place> _Places;
		
		private EntitySet<SportEvent> _SportEvents;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnCategoryIDChanging(int value);
    partial void OnCategoryIDChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnColorChanging(string value);
    partial void OnColorChanged();
    #endregion
		
		public Category()
		{
			this._Places = new EntitySet<Place>(new Action<Place>(this.attach_Places), new Action<Place>(this.detach_Places));
			this._SportEvents = new EntitySet<SportEvent>(new Action<SportEvent>(this.attach_SportEvents), new Action<SportEvent>(this.detach_SportEvents));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int CategoryID
		{
			get
			{
				return this._CategoryID;
			}
			set
			{
				if ((this._CategoryID != value))
				{
					this.OnCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._CategoryID = value;
					this.SendPropertyChanged("CategoryID");
					this.OnCategoryIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Color", DbType="NVarChar(50)")]
		public string Color
		{
			get
			{
				return this._Color;
			}
			set
			{
				if ((this._Color != value))
				{
					this.OnColorChanging(value);
					this.SendPropertyChanging();
					this._Color = value;
					this.SendPropertyChanged("Color");
					this.OnColorChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Category_Place", Storage="_Places", ThisKey="CategoryID", OtherKey="CategoryID")]
		public EntitySet<Place> Places
		{
			get
			{
				return this._Places;
			}
			set
			{
				this._Places.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Category_SportEvent", Storage="_SportEvents", ThisKey="CategoryID", OtherKey="CategoryID")]
		public EntitySet<SportEvent> SportEvents
		{
			get
			{
				return this._SportEvents;
			}
			set
			{
				this._SportEvents.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Places(Place entity)
		{
			this.SendPropertyChanging();
			entity.Category = this;
		}
		
		private void detach_Places(Place entity)
		{
			this.SendPropertyChanging();
			entity.Category = null;
		}
		
		private void attach_SportEvents(SportEvent entity)
		{
			this.SendPropertyChanging();
			entity.Category = this;
		}
		
		private void detach_SportEvents(SportEvent entity)
		{
			this.SendPropertyChanging();
			entity.Category = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Places")]
	public partial class Place : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
		private int _CategoryID;
		
		private string _Description;
		
		private string _Address;
		
		private double _lat;
		
		private double _lng;
		
		private EntitySet<SportEvent> _SportEvents;
		
		private EntityRef<Category> _Category;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnCategoryIDChanging(int value);
    partial void OnCategoryIDChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnAddressChanging(string value);
    partial void OnAddressChanged();
    partial void OnlatChanging(double value);
    partial void OnlatChanged();
    partial void OnlngChanging(double value);
    partial void OnlngChanged();
    #endregion
		
		public Place()
		{
			this._SportEvents = new EntitySet<SportEvent>(new Action<SportEvent>(this.attach_SportEvents), new Action<SportEvent>(this.detach_SportEvents));
			this._Category = default(EntityRef<Category>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryID", DbType="Int NOT NULL")]
		public int CategoryID
		{
			get
			{
				return this._CategoryID;
			}
			set
			{
				if ((this._CategoryID != value))
				{
					if (this._Category.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._CategoryID = value;
					this.SendPropertyChanged("CategoryID");
					this.OnCategoryIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(50)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Address", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if ((this._Address != value))
				{
					this.OnAddressChanging(value);
					this.SendPropertyChanging();
					this._Address = value;
					this.SendPropertyChanged("Address");
					this.OnAddressChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lat", DbType="Float NOT NULL")]
		public double lat
		{
			get
			{
				return this._lat;
			}
			set
			{
				if ((this._lat != value))
				{
					this.OnlatChanging(value);
					this.SendPropertyChanging();
					this._lat = value;
					this.SendPropertyChanged("lat");
					this.OnlatChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_lng", DbType="Float NOT NULL")]
		public double lng
		{
			get
			{
				return this._lng;
			}
			set
			{
				if ((this._lng != value))
				{
					this.OnlngChanging(value);
					this.SendPropertyChanging();
					this._lng = value;
					this.SendPropertyChanged("lng");
					this.OnlngChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Place_SportEvent", Storage="_SportEvents", ThisKey="Id", OtherKey="PlaceID")]
		public EntitySet<SportEvent> SportEvents
		{
			get
			{
				return this._SportEvents;
			}
			set
			{
				this._SportEvents.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Category_Place", Storage="_Category", ThisKey="CategoryID", OtherKey="CategoryID", IsForeignKey=true)]
		public Category Category
		{
			get
			{
				return this._Category.Entity;
			}
			set
			{
				Category previousValue = this._Category.Entity;
				if (((previousValue != value) 
							|| (this._Category.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Category.Entity = null;
						previousValue.Places.Remove(this);
					}
					this._Category.Entity = value;
					if ((value != null))
					{
						value.Places.Add(this);
						this._CategoryID = value.CategoryID;
					}
					else
					{
						this._CategoryID = default(int);
					}
					this.SendPropertyChanged("Category");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_SportEvents(SportEvent entity)
		{
			this.SendPropertyChanging();
			entity.Place = this;
		}
		
		private void detach_SportEvents(SportEvent entity)
		{
			this.SendPropertyChanging();
			entity.Place = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SportEvents")]
	public partial class SportEvent : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _EventID;
		
		private System.DateTime _Date;
		
		private double _Duration;
		
		private int _MaxAttendies;
		
		private int _CategoryID;
		
		private int _OwnerID;
		
		private string _notes;
		
		private string _title;
		
		private string _Users;
		
		private int _PlaceID;
		
		private EntitySet<Users_Event> _Users_Events;
		
		private EntityRef<Category> _Category;
		
		private EntityRef<Place> _Place;
		
		private EntityRef<UserAccount> _UserAccount;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnEventIDChanging(int value);
    partial void OnEventIDChanged();
    partial void OnDateChanging(System.DateTime value);
    partial void OnDateChanged();
    partial void OnDurationChanging(double value);
    partial void OnDurationChanged();
    partial void OnMaxAttendiesChanging(int value);
    partial void OnMaxAttendiesChanged();
    partial void OnCategoryIDChanging(int value);
    partial void OnCategoryIDChanged();
    partial void OnOwnerIDChanging(int value);
    partial void OnOwnerIDChanged();
    partial void OnnotesChanging(string value);
    partial void OnnotesChanged();
    partial void OntitleChanging(string value);
    partial void OntitleChanged();
    partial void OnUsersChanging(string value);
    partial void OnUsersChanged();
    partial void OnPlaceIDChanging(int value);
    partial void OnPlaceIDChanged();
    #endregion
		
		public SportEvent()
		{
			this._Users_Events = new EntitySet<Users_Event>(new Action<Users_Event>(this.attach_Users_Events), new Action<Users_Event>(this.detach_Users_Events));
			this._Category = default(EntityRef<Category>);
			this._Place = default(EntityRef<Place>);
			this._UserAccount = default(EntityRef<UserAccount>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EventID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int EventID
		{
			get
			{
				return this._EventID;
			}
			set
			{
				if ((this._EventID != value))
				{
					this.OnEventIDChanging(value);
					this.SendPropertyChanging();
					this._EventID = value;
					this.SendPropertyChanged("EventID");
					this.OnEventIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Date", DbType="DateTime NOT NULL")]
		public System.DateTime Date
		{
			get
			{
				return this._Date;
			}
			set
			{
				if ((this._Date != value))
				{
					this.OnDateChanging(value);
					this.SendPropertyChanging();
					this._Date = value;
					this.SendPropertyChanged("Date");
					this.OnDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Duration", DbType="Float NOT NULL")]
		public double Duration
		{
			get
			{
				return this._Duration;
			}
			set
			{
				if ((this._Duration != value))
				{
					this.OnDurationChanging(value);
					this.SendPropertyChanging();
					this._Duration = value;
					this.SendPropertyChanged("Duration");
					this.OnDurationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_MaxAttendies", DbType="Int NOT NULL")]
		public int MaxAttendies
		{
			get
			{
				return this._MaxAttendies;
			}
			set
			{
				if ((this._MaxAttendies != value))
				{
					this.OnMaxAttendiesChanging(value);
					this.SendPropertyChanging();
					this._MaxAttendies = value;
					this.SendPropertyChanged("MaxAttendies");
					this.OnMaxAttendiesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CategoryID", DbType="Int NOT NULL")]
		public int CategoryID
		{
			get
			{
				return this._CategoryID;
			}
			set
			{
				if ((this._CategoryID != value))
				{
					if (this._Category.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnCategoryIDChanging(value);
					this.SendPropertyChanging();
					this._CategoryID = value;
					this.SendPropertyChanged("CategoryID");
					this.OnCategoryIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OwnerID", DbType="Int NOT NULL")]
		public int OwnerID
		{
			get
			{
				return this._OwnerID;
			}
			set
			{
				if ((this._OwnerID != value))
				{
					if (this._UserAccount.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnOwnerIDChanging(value);
					this.SendPropertyChanging();
					this._OwnerID = value;
					this.SendPropertyChanged("OwnerID");
					this.OnOwnerIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_notes", DbType="NVarChar(MAX)")]
		public string notes
		{
			get
			{
				return this._notes;
			}
			set
			{
				if ((this._notes != value))
				{
					this.OnnotesChanging(value);
					this.SendPropertyChanging();
					this._notes = value;
					this.SendPropertyChanged("notes");
					this.OnnotesChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_title", DbType="NVarChar(50)")]
		public string title
		{
			get
			{
				return this._title;
			}
			set
			{
				if ((this._title != value))
				{
					this.OntitleChanging(value);
					this.SendPropertyChanging();
					this._title = value;
					this.SendPropertyChanged("title");
					this.OntitleChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Users", DbType="NVarChar(MAX)")]
		public string Users
		{
			get
			{
				return this._Users;
			}
			set
			{
				if ((this._Users != value))
				{
					this.OnUsersChanging(value);
					this.SendPropertyChanging();
					this._Users = value;
					this.SendPropertyChanged("Users");
					this.OnUsersChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PlaceID", DbType="Int NOT NULL")]
		public int PlaceID
		{
			get
			{
				return this._PlaceID;
			}
			set
			{
				if ((this._PlaceID != value))
				{
					if (this._Place.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPlaceIDChanging(value);
					this.SendPropertyChanging();
					this._PlaceID = value;
					this.SendPropertyChanged("PlaceID");
					this.OnPlaceIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="SportEvent_Users_Event", Storage="_Users_Events", ThisKey="EventID", OtherKey="EventID")]
		public EntitySet<Users_Event> Users_Events
		{
			get
			{
				return this._Users_Events;
			}
			set
			{
				this._Users_Events.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Category_SportEvent", Storage="_Category", ThisKey="CategoryID", OtherKey="CategoryID", IsForeignKey=true)]
		public Category Category
		{
			get
			{
				return this._Category.Entity;
			}
			set
			{
				Category previousValue = this._Category.Entity;
				if (((previousValue != value) 
							|| (this._Category.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Category.Entity = null;
						previousValue.SportEvents.Remove(this);
					}
					this._Category.Entity = value;
					if ((value != null))
					{
						value.SportEvents.Add(this);
						this._CategoryID = value.CategoryID;
					}
					else
					{
						this._CategoryID = default(int);
					}
					this.SendPropertyChanged("Category");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Place_SportEvent", Storage="_Place", ThisKey="PlaceID", OtherKey="Id", IsForeignKey=true)]
		public Place Place
		{
			get
			{
				return this._Place.Entity;
			}
			set
			{
				Place previousValue = this._Place.Entity;
				if (((previousValue != value) 
							|| (this._Place.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Place.Entity = null;
						previousValue.SportEvents.Remove(this);
					}
					this._Place.Entity = value;
					if ((value != null))
					{
						value.SportEvents.Add(this);
						this._PlaceID = value.Id;
					}
					else
					{
						this._PlaceID = default(int);
					}
					this.SendPropertyChanged("Place");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="UserAccount_SportEvent", Storage="_UserAccount", ThisKey="OwnerID", OtherKey="UserID", IsForeignKey=true)]
		public UserAccount UserAccount
		{
			get
			{
				return this._UserAccount.Entity;
			}
			set
			{
				UserAccount previousValue = this._UserAccount.Entity;
				if (((previousValue != value) 
							|| (this._UserAccount.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._UserAccount.Entity = null;
						previousValue.SportEvents.Remove(this);
					}
					this._UserAccount.Entity = value;
					if ((value != null))
					{
						value.SportEvents.Add(this);
						this._OwnerID = value.UserID;
					}
					else
					{
						this._OwnerID = default(int);
					}
					this.SendPropertyChanged("UserAccount");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Users_Events(Users_Event entity)
		{
			this.SendPropertyChanging();
			entity.SportEvent = this;
		}
		
		private void detach_Users_Events(Users_Event entity)
		{
			this.SendPropertyChanging();
			entity.SportEvent = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Users_Events")]
	public partial class Users_Event : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Event_User_ID;
		
		private int _EventID;
		
		private int _UserID;
		
		private EntityRef<SportEvent> _SportEvent;
		
		private EntityRef<UserAccount> _UserAccount;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnEvent_User_IDChanging(int value);
    partial void OnEvent_User_IDChanged();
    partial void OnEventIDChanging(int value);
    partial void OnEventIDChanged();
    partial void OnUserIDChanging(int value);
    partial void OnUserIDChanged();
    #endregion
		
		public Users_Event()
		{
			this._SportEvent = default(EntityRef<SportEvent>);
			this._UserAccount = default(EntityRef<UserAccount>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Event_User_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Event_User_ID
		{
			get
			{
				return this._Event_User_ID;
			}
			set
			{
				if ((this._Event_User_ID != value))
				{
					this.OnEvent_User_IDChanging(value);
					this.SendPropertyChanging();
					this._Event_User_ID = value;
					this.SendPropertyChanged("Event_User_ID");
					this.OnEvent_User_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_EventID", DbType="Int NOT NULL")]
		public int EventID
		{
			get
			{
				return this._EventID;
			}
			set
			{
				if ((this._EventID != value))
				{
					if (this._SportEvent.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnEventIDChanging(value);
					this.SendPropertyChanging();
					this._EventID = value;
					this.SendPropertyChanged("EventID");
					this.OnEventIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="Int NOT NULL")]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					if (this._UserAccount.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnUserIDChanging(value);
					this.SendPropertyChanging();
					this._UserID = value;
					this.SendPropertyChanged("UserID");
					this.OnUserIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="SportEvent_Users_Event", Storage="_SportEvent", ThisKey="EventID", OtherKey="EventID", IsForeignKey=true)]
		public SportEvent SportEvent
		{
			get
			{
				return this._SportEvent.Entity;
			}
			set
			{
				SportEvent previousValue = this._SportEvent.Entity;
				if (((previousValue != value) 
							|| (this._SportEvent.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._SportEvent.Entity = null;
						previousValue.Users_Events.Remove(this);
					}
					this._SportEvent.Entity = value;
					if ((value != null))
					{
						value.Users_Events.Add(this);
						this._EventID = value.EventID;
					}
					else
					{
						this._EventID = default(int);
					}
					this.SendPropertyChanged("SportEvent");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="UserAccount_Users_Event", Storage="_UserAccount", ThisKey="UserID", OtherKey="UserID", IsForeignKey=true)]
		public UserAccount UserAccount
		{
			get
			{
				return this._UserAccount.Entity;
			}
			set
			{
				UserAccount previousValue = this._UserAccount.Entity;
				if (((previousValue != value) 
							|| (this._UserAccount.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._UserAccount.Entity = null;
						previousValue.Users_Events.Remove(this);
					}
					this._UserAccount.Entity = value;
					if ((value != null))
					{
						value.Users_Events.Add(this);
						this._UserID = value.UserID;
					}
					else
					{
						this._UserID = default(int);
					}
					this.SendPropertyChanged("UserAccount");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591

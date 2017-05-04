﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace ARMS_W.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class Model1Container : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new Model1Container object using the connection string found in the 'Model1Container' section of the application configuration file.
        /// </summary>
        public Model1Container() : base("name=Model1Container", "Model1Container")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new Model1Container object.
        /// </summary>
        public Model1Container(string connectionString) : base(connectionString, "Model1Container")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new Model1Container object.
        /// </summary>
        public Model1Container(EntityConnection connection) : base(connection, "Model1Container")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<customerLeadI> customerLeadIs
        {
            get
            {
                if ((_customerLeadIs == null))
                {
                    _customerLeadIs = base.CreateObjectSet<customerLeadI>("customerLeadIs");
                }
                return _customerLeadIs;
            }
        }
        private ObjectSet<customerLeadI> _customerLeadIs;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the customerLeadIs EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddTocustomerLeadIs(customerLeadI customerLeadI)
        {
            base.AddObject("customerLeadIs", customerLeadI);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Model1", Name="customerLeadI")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class customerLeadI : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new customerLeadI object.
        /// </summary>
        /// <param name="requestId">Initial value of the RequestId property.</param>
        /// <param name="status">Initial value of the Status property.</param>
        /// <param name="proposedChannel">Initial value of the ProposedChannel property.</param>
        /// <param name="ccaNum">Initial value of the ccaNum property.</param>
        /// <param name="bkToSender">Initial value of the BkToSender property.</param>
        /// <param name="region">Initial value of the Region property.</param>
        /// <param name="encodedBy">Initial value of the EncodedBy property.</param>
        /// <param name="tAG1">Initial value of the TAG1 property.</param>
        public static customerLeadI CreatecustomerLeadI(global::System.String requestId, global::System.String status, global::System.String proposedChannel, global::System.String ccaNum, global::System.String bkToSender, global::System.String region, global::System.String encodedBy, global::System.String tAG1)
        {
            customerLeadI customerLeadI = new customerLeadI();
            customerLeadI.RequestId = requestId;
            customerLeadI.Status = status;
            customerLeadI.ProposedChannel = proposedChannel;
            customerLeadI.ccaNum = ccaNum;
            customerLeadI.BkToSender = bkToSender;
            customerLeadI.Region = region;
            customerLeadI.EncodedBy = encodedBy;
            customerLeadI.TAG1 = tAG1;
            return customerLeadI;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String RequestId
        {
            get
            {
                return _RequestId;
            }
            set
            {
                if (_RequestId != value)
                {
                    OnRequestIdChanging(value);
                    ReportPropertyChanging("RequestId");
                    _RequestId = StructuralObject.SetValidValue(value, false);
                    ReportPropertyChanged("RequestId");
                    OnRequestIdChanged();
                }
            }
        }
        private global::System.String _RequestId;
        partial void OnRequestIdChanging(global::System.String value);
        partial void OnRequestIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Status
        {
            get
            {
                return _Status;
            }
            set
            {
                OnStatusChanging(value);
                ReportPropertyChanging("Status");
                _Status = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Status");
                OnStatusChanged();
            }
        }
        private global::System.String _Status;
        partial void OnStatusChanging(global::System.String value);
        partial void OnStatusChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ProposedChannel
        {
            get
            {
                return _ProposedChannel;
            }
            set
            {
                OnProposedChannelChanging(value);
                ReportPropertyChanging("ProposedChannel");
                _ProposedChannel = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ProposedChannel");
                OnProposedChannelChanged();
            }
        }
        private global::System.String _ProposedChannel;
        partial void OnProposedChannelChanging(global::System.String value);
        partial void OnProposedChannelChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ccaNum
        {
            get
            {
                return _ccaNum;
            }
            set
            {
                OnccaNumChanging(value);
                ReportPropertyChanging("ccaNum");
                _ccaNum = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ccaNum");
                OnccaNumChanged();
            }
        }
        private global::System.String _ccaNum;
        partial void OnccaNumChanging(global::System.String value);
        partial void OnccaNumChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String BkToSender
        {
            get
            {
                return _BkToSender;
            }
            set
            {
                OnBkToSenderChanging(value);
                ReportPropertyChanging("BkToSender");
                _BkToSender = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("BkToSender");
                OnBkToSenderChanged();
            }
        }
        private global::System.String _BkToSender;
        partial void OnBkToSenderChanging(global::System.String value);
        partial void OnBkToSenderChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Region
        {
            get
            {
                return _Region;
            }
            set
            {
                OnRegionChanging(value);
                ReportPropertyChanging("Region");
                _Region = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Region");
                OnRegionChanged();
            }
        }
        private global::System.String _Region;
        partial void OnRegionChanging(global::System.String value);
        partial void OnRegionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String EncodedBy
        {
            get
            {
                return _EncodedBy;
            }
            set
            {
                OnEncodedByChanging(value);
                ReportPropertyChanging("EncodedBy");
                _EncodedBy = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("EncodedBy");
                OnEncodedByChanged();
            }
        }
        private global::System.String _EncodedBy;
        partial void OnEncodedByChanging(global::System.String value);
        partial void OnEncodedByChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String TAG1
        {
            get
            {
                return _TAG1;
            }
            set
            {
                OnTAG1Changing(value);
                ReportPropertyChanging("TAG1");
                _TAG1 = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("TAG1");
                OnTAG1Changed();
            }
        }
        private global::System.String _TAG1;
        partial void OnTAG1Changing(global::System.String value);
        partial void OnTAG1Changed();

        #endregion
    
    }

    #endregion
    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using Library.Common.Extension;
using System.Windows.Input;

namespace Demo.Content
{
    public enum FieldType
    {
        Field,
        Group
    }

    public class ReportFieldsVM:NotificationObject
    {
        private ReportDataManagementVM _parent;
        private FieldType _fieldType;

        public FieldType ReportFieldType
        {
            get { return _fieldType; }
        }

        public ReportFieldsVM(ReportDataManagementVM parent)
        {
            _parent = parent;
            _fieldType = FieldType.Field;

            AddMenu = "Add a new field";
            EditMenu = "Edit the field";
            DeleteMenu = "Remove the field";

            AddItemCommand = new DelegateCommand<object>(x => { AddReportField(); },
                x=> CanAddReportField());
            EditItemCommand = new DelegateCommand<object>(x => { EditReportField(); },
                x => { return CanEditReportField(); });
            RemoveItemCommand = new DelegateCommand<object>(x => { RemoveReportField(); },
                x => { return CanRemoveReportField(); });

            Items.CollectionChanged += (d, e) => { CommandManager.InvalidateRequerySuggested(); };
        }

        public ReportFieldsVM(ReportDataManagementVM parent,FieldType fieldType)
        {
            _parent = parent;
            _fieldType = fieldType;

            AddMenu = "Add a new group";
            EditMenu = "Edit the group";
            DeleteMenu = "Remove the group";

            AddItemCommand = new DelegateCommand<object>(x => { AddReportField(); },
                x => CanAddReportField());
            EditItemCommand = new DelegateCommand<object>(x => { EditReportField(); },
                x => { return CanEditReportField(); });
            RemoveItemCommand = new DelegateCommand<object>(x => { RemoveReportField(); },
                x => { return CanRemoveReportField(); });

            Items.CollectionChanged += (d, e) => { CommandManager.InvalidateRequerySuggested(); };
        }

        #region Properties

        private ObservableCollection<ReportFieldInteractionVM> _items = new ObservableCollection<ReportFieldInteractionVM>();
        public ObservableCollection<ReportFieldInteractionVM> Items
        {
            get { return _items; }
        }

        private ReportFieldInteractionVM _selectedItem;
        public ReportFieldInteractionVM SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                this.RaisePropertyChanged(() => this.SelectedItem);
                this.EditItemCommand.RaiseCanExecuteChanged();
                this.RemoveItemCommand.RaiseCanExecuteChanged();
            }
        }

        public string AddMenu { get; set; }
        public string EditMenu { get; set; }
        public string DeleteMenu { get; set; }

        #endregion

        #region Commands

        public DelegateCommand<object> AddItemCommand { get; set; }
        public DelegateCommand<object> EditItemCommand { get; set; }
        public DelegateCommand<object> RemoveItemCommand { get; set; }

        #endregion

        #region Functions

        private void AddReportField()
        {
            var field = new ReportFieldInteractionVM(_parent.FieldTypeAdapterFactory,
                new ObservableCollection<ReportFieldVM>(_parent.GroupFeilds.Items.Select(x => x.ReportField)), this);

            if (_fieldType == FieldType.Field)
            {
                field.Title = "Add the Report Field";
                _parent.SendRequestToField(field);
            }
            else
            {
                field.Title = "Add the Report Group";
                _parent.SendRequestToGroup(field);
            }


        }

        private bool CanAddReportField()
        {
            return !string.IsNullOrEmpty(_parent.ReportName);
        }

        private bool CanEditReportField()
        {
            return this.Items.Any() || !this.SelectedItem.IsNull();
        }

        public void EditReportField()
        {
            if (this.SelectedItem == null)
                return;

            if (_fieldType == FieldType.Field)
            {
                this.SelectedItem.Title = "Edit the Report Field";
                _parent.SendRequestToField(this.SelectedItem);
            }
            else
            {
                this.SelectedItem.Title = "Edit the Group Field";
                _parent.SendRequestToGroup(this.SelectedItem);
            }

        }

        private bool CanRemoveReportField()
        {
            return this.Items.Any() || !this.SelectedItem.IsNull();
        }

        private void RemoveReportField()
        {
            if (this.SelectedItem == null)
                return;

            this.Items.Remove(this.SelectedItem);
        }

        #endregion

    }
}

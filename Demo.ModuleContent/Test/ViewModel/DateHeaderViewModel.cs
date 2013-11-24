using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using System.Threading;
using CdcSoftware.Erp.Rrs;
using CdcSoftware.Erp.Rrs.Server;
using Library.Common.Extension;

namespace Demo.Content.Test
{
  [Export]
  public class DateHeaderViewModel : NotificationObject
  {

    private WeekHeader _weekHeader;

    private DateHeaderFactory _factory;

    public DateHeaderViewModel()
    {
      this.StartDate = DateTime.Now;

      this._dayHeaders = new ObservableCollection<string>();
      this._weekHeaders = new ObservableCollection<string>();
      this._originalWeekHeaders = new ObservableCollection<string>();
      this._monthHeaders = new ObservableCollection<string>();
      this._fourWeekHeaders = new ObservableCollection<string>();

      this.CreateDayHeaderCommand = new DelegateCommand<object>((o) => CreateDayHeader());
      this.CreateWeekHeaderCommand = new DelegateCommand<object>((o) => CreateWeekHeaders());
      this.CreateOriginalWeekHeadersCommand = new DelegateCommand<object>((o) => CreateOriginalWeekHeaders());
      this.CreateMonthHeaderCommand = new DelegateCommand<object>((o) => CreateMonthHeaders());
      this.Create4WeekHeaderCommand = new DelegateCommand<object>((o) => Create4WeekHeaders());
    }

    private DateTime _startDate;
    public DateTime StartDate
    {
      get { return _startDate; }
      set
      {
        if (_startDate == value)
          return;
        _startDate = value;
        _weekHeader =  new WeekHeader(7, this.StartDate);
        _factory = new DateHeaderFactory(null, _startDate);
        this.RaisePropertyChanged(() => StartDate);
      }
    }


    private ObservableCollection<string> _dayHeaders;
    public ObservableCollection<string> DayHeaders
    {
      get { return _dayHeaders; }
    }

    private ObservableCollection<string> _weekHeaders;
    public ObservableCollection<string> WeekHeaders
    {
      get { return _weekHeaders; }
    }

    private ObservableCollection<string> _monthHeaders;
    public ObservableCollection<string> MonthHeaders
    {
      get { return _monthHeaders; }
    }

    private ObservableCollection<string> _fourWeekHeaders;
    public ObservableCollection<string> FourWeekHeaders
    { get { return _fourWeekHeaders; } }


    public ObservableCollection<string> _originalWeekHeaders;
    public ObservableCollection<string> OriginalWeekHeaders
    { get { return _originalWeekHeaders; } }


    public DelegateCommand<object> CreateDayHeaderCommand { get; set; }
    public DelegateCommand<object> CreateWeekHeaderCommand { get; set; }
    public DelegateCommand<object> CreateOriginalWeekHeadersCommand { get; set; }
    public DelegateCommand<object> CreateMonthHeaderCommand { get; set; }
    public DelegateCommand<object> Create4WeekHeaderCommand { get; set; }

    public void CreateDayHeader()
    {
      var header = _factory.GetDayHeader();
      if (DayHeaders.Count > 0)
        this.DayHeaders.Clear();

      AddItems(this.DayHeaders, header);
    }

    private void AddItems(ObservableCollection<string> headers, WeekHeaderItem header)
    {
      headers.Add(header.Column1.DefaultIfNull("empty"));
      headers.Add(header.Column2.DefaultIfNull("empty"));
      headers.Add(header.Column3.DefaultIfNull("empty"));
      headers.Add(header.Column4.DefaultIfNull("empty"));
      headers.Add(header.Column5.DefaultIfNull("empty"));
      headers.Add(header.Column6.DefaultIfNull("empty"));
      headers.Add(header.Column7.DefaultIfNull("empty"));
      headers.Add(header.Column8.DefaultIfNull("empty"));
      headers.Add(header.Column9.DefaultIfNull("empty"));
      headers.Add(header.Column10.DefaultIfNull("empty"));
      headers.Add(header.Column11.DefaultIfNull("empty"));
      headers.Add(header.Column12.DefaultIfNull("empty"));
      headers.Add(header.Column13.DefaultIfNull("empty"));
      headers.Add(header.Column14.DefaultIfNull("empty"));
    }

    public void CreateWeekHeaders()
    {
      var header = _factory.GetWeekHeader();
      if (this.WeekHeaders.Count > 0)
        this.WeekHeaders.Clear();

      AddItems(this.WeekHeaders, header);
    }

    public void CreateOriginalWeekHeaders()
    {
      var headers = _weekHeader.GetWeekHeaders();

      if (OriginalWeekHeaders.Count > 0)
        OriginalWeekHeaders.Clear();

      foreach (var header in headers)
        OriginalWeekHeaders.Add(header);

    }

    public void CreateMonthHeaders()
    {
      var header = _factory.GetMonthHeader();

      if (this.MonthHeaders.Count > 0)
        this.MonthHeaders.Clear();

      AddItems(this.MonthHeaders, header);
    }

    public void Create4WeekHeaders()
    {
      var header = _factory.Get4WeekHeader();

      if (this.FourWeekHeaders.Count > 0)
        this.FourWeekHeaders.Clear();

      AddItems(this.FourWeekHeaders, header);
    }

  }
}

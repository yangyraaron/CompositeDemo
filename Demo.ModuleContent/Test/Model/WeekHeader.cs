using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Demo.Content.Test
{
    public class WeekHeader
    {

            #region Constant String
            public const string Overdue = "Overdue";
            public const string SplitStr = "||";
            public const string DateFormatStr = "MM/dd/yyyy";
            #endregion

            #region Private field
            /// <summary>
            ///  _step = 1 indicate "by day" showing mode, 7 indicate "by week", 28 indicate "by 4-weeks"
            /// </summary>
            private int _step;
            /// <summary>
            /// _firstStep indicate the number of current day of week,if today was Wenesday, then _firstStep = 3
            /// </summary>
            private int _firstStep;
            /// <summary>
            /// _currentDate indicate the starting date of the fixed-14-columns header date, it is often initialized by Now 
            /// </summary>
            private DateTime _currentDate;
            /// <summary>
            /// using this Calendar object to generate the week header text.
            /// </summary>
            private static Calendar _myCal;
            /// <summary>
            /// which day is the first day of week.
            /// </summary>
            private static DayOfWeek _myFirstDOW;
            /// <summary>
            /// using this week rule to generate the week header text,
            /// we can choose the FirstFourDayWeek, FirstDayWeek or FirstFullWeek rule
            /// </summary>
            private static CalendarWeekRule _myCWR;


            #endregion

            #region Constructor

            static WeekHeader()
            {

                _myCal = CultureInfo.CurrentCulture.Calendar;
                _myFirstDOW = DayOfWeek.Monday;
                _myCWR = CalendarWeekRule.FirstFourDayWeek;
            }


            /// <summary>
            /// Set and return an instance of WeekHeader.
            /// </summary>
            /// <param name="step">step = 1 indicate "by day" showing mode, 7 indicate "by week", 28 indicate "by 4-weeks"</param>
            /// <param name="startDate">The start date of WeekHeader period.</param>
            public WeekHeader(int step, DateTime startDate)
            {
                _step = step;
                _currentDate = startDate;

                switch (step)
                {
                    case 1: _firstStep = 1; break;
                    case 7: _firstStep = ((int)_currentDate.DayOfWeek) == 0 ? 7 : (int)_currentDate.DayOfWeek; break;
                    default: _firstStep = 0; break;
                }
            }

            #endregion

            public string[] GetWeekHeaders()
            {
                string[] weekHeaders = new string[15];

                weekHeaders[0] = Overdue;

                for (int i = 1; i < 15; i++)
                {
                    weekHeaders[i] = GetWeekHeader(i);
                }

                return weekHeaders;
            }

            private string GetWeekHeader(int index)
            {

                switch (_step)
                {
                    case 1: return _currentDate.AddDays(index - 1).ToString(DateFormatStr);
                    case 7:
                        {
                            StringBuilder strb = new StringBuilder();
                            if (index == 1)
                            {
                                DateTime endingDate = _currentDate.AddDays(7 - _firstStep);
                                DateTime firstDayDate = DateTime.Parse("1/1/" + endingDate.Year.ToString()).Date;
                                TimeSpan timeSpan = endingDate - firstDayDate;

                                //if the span days between first day of year and ending day of current column is 3-7
                                //,then hard coding the current column week number as 1
                                if (timeSpan.Days < 7 && timeSpan.Days >= 3)
                                {
                                    strb.Append("1");
                                    strb.Append(SplitStr);
                                    strb.Append(_currentDate.ToString(DateFormatStr));
                                    strb.Append(SplitStr);
                                    strb.Append(endingDate.ToString(DateFormatStr));
                                    return strb.ToString();
                                }

                                strb.Append(_myCal.GetWeekOfYear(_currentDate, _myCWR, _myFirstDOW).ToString());
                                strb.Append(SplitStr);
                                strb.Append(_currentDate.ToString(DateFormatStr));
                                strb.Append(SplitStr);
                                strb.Append(endingDate.ToString(DateFormatStr));

                                return strb.ToString();
                            }
                            else
                            {
                                DateTime runDate = _currentDate.AddDays(8 - _firstStep + (index - 2) * _step);
                                DateTime endingDate = runDate.AddDays(6).Date;
                                DateTime firstDayDate = DateTime.Parse("1/1/" + endingDate.Year.ToString()).Date;
                                TimeSpan timeSpan = endingDate - firstDayDate;

                                //if the span days between first day of year and ending day of current column is 3-7
                                //,then hard coding the current column week number as 1
                                if (timeSpan.Days < 7 && timeSpan.Days >= 3)
                                {
                                    strb.Append("1");
                                    strb.Append(SplitStr);
                                    strb.Append(runDate.ToString(DateFormatStr));
                                    strb.Append(SplitStr);
                                    strb.Append(endingDate.ToString(DateFormatStr));
                                    return strb.ToString();
                                }

                                strb.Append(_myCal.GetWeekOfYear(runDate, _myCWR, _myFirstDOW).ToString());
                                strb.Append(SplitStr);
                                strb.Append(runDate.ToString(DateFormatStr));
                                strb.Append(SplitStr);
                                strb.Append(endingDate.ToString(DateFormatStr));
                                return strb.ToString();
                            }
                        }
                    default: break;
                }
                return string.Empty;
            }
        }
    
}

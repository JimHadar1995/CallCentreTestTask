﻿using CallCentreConsoleProject.Internal.Comparers;
using CallCentreConsoleProject.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CallCentreConsoleProject.Internal
{
    internal sealed class CallCentreWorker
    {
        private readonly string _filePath;
        private IReadOnlyList<CallCentreRecord> _records;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">Путь к файлу с данными</param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CallCentreWorker(string filePath)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            ThrowHelper.ThrowIfInvalidFilePath(filePath);
            _filePath = filePath;
        }

        /// <summary>
        /// Обработка для первой части
        /// </summary>
        public void PrintPart1()
        {
            Parse();

            Console.WriteLine("###################################################################################################");
            Console.WriteLine("##                                          Отчет №1                                             ##");
            Console.WriteLine("###################################################################################################");

            var sw = new Stopwatch();

            sw.Start();

            var dates = GetDates();

            foreach (var date in dates)
            {
                var dateRecords = _records.Where(_ => _.DateFrom >= date && _.DateFrom <= date.AddDays(1).AddSeconds(-1)).ToList();

                int maximum = 0;

                var endDateTimes = new List<DateTime>(dateRecords.Count);

                foreach (var dateRecord in dateRecords)
                {
                    endDateTimes.Add(dateRecord.DateTo);

                    while (endDateTimes.Min() < dateRecord.DateFrom)
                    {
                        endDateTimes.Remove(endDateTimes.Min());
                    }

                    maximum = Math.Max(maximum, endDateTimes.Count);
                }

                Console.WriteLine($"{date:d} {maximum}");
            }
            sw.Stop();

            Console.WriteLine($"Затраченное время на первую часть: {sw.Elapsed.TotalSeconds} секунд");
        }

        /// <summary>
        /// Обработка для второй части
        /// </summary>
        public void PrintPart2()
        {
            Parse();

            Console.WriteLine(Environment.NewLine +
                "###################################################################################################");
            Console.WriteLine("##                                          Отчет №2                                             ##");
            Console.WriteLine("###################################################################################################" +
                Environment.NewLine);

            var sw = new Stopwatch();
            sw.Start();

            foreach (var groupedByOperator in _records.GroupBy(_ => _.Operator))
            {
                var groupByState = groupedByOperator.GroupBy(_ => _.State);

                HashSet<Tuple<string, int>> states = new HashSet<Tuple<string, int>>(groupByState.Count());

                var res = groupByState.Select(g => $"{Math.Round((double)g.Sum(r => r.Duration) / 60, 2)} минут в состоянии {g.Key}").ToList();

                Console.WriteLine($"{groupedByOperator.Key} {string.Join(' ', res)}");
            }
            sw.Stop();

            Console.WriteLine($"Затраченное время на вторую часть: {sw.Elapsed.TotalSeconds} секунд");
        }

        #region [ Help methods ]

        private IReadOnlyList<DateTime> GetDates()
        {
            var minday = _records.Min(_ => _.DateFrom);
            var maxday = _records.Max(_ => _.DateTo);

            var days = Enumerable.Range(0, 1 + maxday.Date.Subtract(minday.Date).Days)
               .Select(offset => minday.AddDays(offset))
               .ToList();

            return days;
        }

        private void Parse()
        {
            if (_records == null)
            {
                var sw = new Stopwatch();
                sw.Start();
                _records = CsvHelper.Parse(_filePath);
                sw.Stop();
                Console.WriteLine($"Затраченное время на чтение файла: {sw.Elapsed.TotalSeconds} секунд");
            }
        }

        #endregion
    }
}

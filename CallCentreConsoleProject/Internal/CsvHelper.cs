﻿using CallCentreConsoleProject.Internal.Comparers;
using CallCentreConsoleProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CallCentreConsoleProject.Internal
{
    /// <summary>
    /// Парсер только под конкретный шаблон файла. Универсальности нет, по задаче она не требовалась
    /// </summary>
    internal static class CsvHelper
    {
        const string Separator = ";";

        public static List<CallCentreRecord> Parse(string filePath)
        {
            ThrowHelper.ThrowIfInvalidFilePath(filePath);
            using var reader = File.OpenText(filePath);

            return Parse(reader);
        }

        private static List<CallCentreRecord> Parse(StreamReader reader)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            //читаем первую строку, чтобы пропустить заголовок
            reader.ReadLine();
            var records = new List<CallCentreRecord>(100000);
            while (!reader.EndOfStream)
            {
                string? line = reader.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var values = line.Split(Separator);
                    var record = new CallCentreRecord(
                        DateTime.Parse(values[0]),
                        DateTime.Parse(values[1]),
                        values[2],
                        values[3],
                        values[4],
                        int.TryParse(values[5], out var dur) ? dur : 0);
                    records.Add(record);
                }
            }

            records.Sort(new DateFromRecordComparer());

            return records;
        }
        
    }
}

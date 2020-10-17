﻿using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VISMA.TestTask.Core.Data
{
    public class DataGridResult<T>
    {
        public delegate object WrapDelegate(T item, int rowNumber);

        public List<object> Collection { get; private set; }
        public int TotalPageCount { get; private set; }

        public DataGridResult(IEnumerable<T> data, int totalRowCount, int pageNumber, int pageSize, WrapDelegate wrapper)
        {
            TotalPageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalRowCount) / pageSize));
            Collection = data.Select(o => wrapper(o, pageSize * pageNumber + data.IndexOf(o) + 1)).ToList();
        }
    }

}

using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace AllowanceFunctions.Common
{
    public abstract class TableRow<T> : TableEntity
        where T : Entity
    {
        public TableRow() {}
        public TableRow(T entity)
        {
            MapFromEntity(entity);
        }

        public T Entity
        {
            get
            {
                return MapToEntity();
            }
            set
            {
                MapFromEntity(value);
            }
        }

        protected abstract T MapToEntity();
        protected abstract void MapFromEntity(T entity);
    }
}

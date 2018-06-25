using System;
using mCore.Services.DataCenter.Core.Schema;
using Xunit;

namespace mCore.Services.DataCenter.Tests.Core.Schema
{
    public class TableTest
    {
        [Fact]
        public void CreateTableTest()
        {
            var table = CreateTable();

            // the initial table has two columns,
            Assert.Equal(2, table.Columns.Count);
            // one primary key
            Assert.NotNull(table.GetPrimaryKey());
            // one deleted flag column
            Assert.NotNull(table.GetDeletedFlag());
        }

        [Fact]
        public void GetPrimaryKeyTest()
        {
            var table = CreateTable();

            // name and column name equals id.
            Assert.Equal("标识", table.GetPrimaryKey().Name);
            Assert.Equal("Id", table.GetPrimaryKey().ColumnName);
        }

        [Fact]
        public void GetPrimaryKeyExpectExceptionTest()
        {
            var table = CreateTable();
            table.Columns.Clear();

            Assert.Throws<InvalidOperationException>(
                () => table.GetPrimaryKey());
        }

        [Fact]
        public void GetDeletedFlagTest()
        {
            var table = CreateTable();

            Assert.Equal("是否删除", table.GetDeletedFlag().Name);
            Assert.Equal("IsDelete", table.GetDeletedFlag().ColumnName);

            table.Columns.Clear();

            Assert.Throws<InvalidOperationException>(
                () => table.GetDeletedFlag());
        }

        private Table CreateTable()
        {
            return new Table("Test Table");
        }
    }
}
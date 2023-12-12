using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MiniORM
{
    public class MyORM<G, T> : Base<G>
    {
        private readonly string _connectionString;
        private readonly string _tableName;
        private readonly MyORM<G, T> _orm;
        static string mainObject = null;
        static bool mainObjectNameInitialize = true;
        public MyORM()
        {
            _connectionString = "Server=.\\SQLEXPRESS;Database=ORMTest;User Id=ORMTest;Password=123456;Trust Server Certificate=True;";
            _tableName = typeof(T).Name;

        }
        private bool IsClassOfCurrentAssemblyOrCollectionOrArray(PropertyInfo property)
        {
            return (IsClassOfCurrentAssembly(property) || IsCollectionOrArray(property));
        }

        private bool IsClassOfCurrentAssembly(PropertyInfo property)
        {
            return property.PropertyType.IsClass
                    && (property.PropertyType.Assembly.FullName
                    == Assembly.GetExecutingAssembly().FullName); 
        }

        private bool IsCollectionOrArray(PropertyInfo property)
        {
            return (property.PropertyType.Namespace is ("System.Collections.Generic"
                    or "System.Collections")) || property.PropertyType.IsArray;
        }

        //public void Insert<T>(T item)
        //{
        //    //InsertNestedData(item);
        //    var mainObjectName = item.GetType().Name;
        //    if (mainObjectNameInitialize)
        //    {
        //        mainObject = mainObjectName;
        //        mainObjectNameInitialize = false;
        //    }

        //    var columnNames = getAllColumns(mainObjectName);

        //    foreach (PropertyInfo property in item.GetType().GetProperties())
        //    {
        //        if (property.PropertyType.IsGenericType &&
        //            typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
        //        {
        //            var nestedData = property.GetValue(item);
        //            if (nestedData != null)
        //            {


        //                foreach (var element in nestedData as IEnumerable)
        //                {
        //                    var tableName = element.GetType().Name;
        //                    var foreignKeyColumn = getAllColumns(tableName).Where(x => x == mainObjectName + "Id").Select(x => x);
        //                    //                        var foreignKeyField = element.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
        //                    //.FirstOrDefault(f => f.FieldType.IsClass && f.Name.EndsWith(mainObjectName + "Id"));
        //                    object foreignKeyValue = null;
        //                    if (foreignKeyColumn != null)
        //                    {
        //                        var fkValue = item.GetType().GetProperty("Id").GetValue(item);
        //                        foreignKeyValue = fkValue;
        //                        //foreignKeyColumn.SetValue(element, fkValue);
        //                    }
        //                    InsertListType(element, foreignKeyValue,item);


        //                }

        //            }
        //        }
        //        else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
        //        {
        //            var nestedData = property.GetValue(item);
        //            if (nestedData != null)
        //            {
        //                PropertyInfo foreignKeyProperty = nestedData.GetType().GetProperty(mainObjectName + "Id");
        //                if (foreignKeyProperty != null)
        //                {
        //                    object foreignKeyValue = item.GetType().GetProperty("Id").GetValue(item);
        //                    foreignKeyProperty.SetValue(nestedData, foreignKeyValue);
        //                }
        //                Insert(nestedData);
        //            }
        //        }
        //    }

        //    var sql = new StringBuilder("Insert into ");
        //    sql.Append(mainObjectName).Append('(');
        //    foreach (var column in columnNames)
        //    {
        //        sql.Append(column).Append(',');
        //    }
        //    sql.Remove(sql.Length - 1, 1).Append(") values(");
        //    foreach (var column in columnNames)
        //    {
        //        sql.Append('@').Append(column).Append(',');
        //    }
        //    sql.Remove(sql.Length - 1, 1).Append(");");
        //    var query = sql.ToString();

        //    var command = GetCommand(query);
        //    foreach (var column in columnNames)
        //    {
        //        foreach (var property in item.GetType().GetProperties())
        //        {
        //            if (!IsClassOfCurrentAssemblyOrCollectionOrArray(property) && column == property.Name)
        //            {
        //                command.Parameters.AddWithValue("@" + column, property.GetValue(item));
        //            }
        //            else if (property.PropertyType.IsClass && property.PropertyType != typeof(string) &&
        //                     !typeof(IEnumerable).IsAssignableFrom(property.PropertyType) &&
        //                     column.Contains(property.Name))
        //            {
        //                Type nestedType = property.PropertyType;
        //                PropertyInfo nestedIdProperty = nestedType.GetProperty("Id");
        //                object nestedIdValue = nestedIdProperty.GetValue(property.GetValue(item));
        //                command.Parameters.AddWithValue("@" + column, nestedIdValue);
        //            }

        //        }
        //    }
        //    command.ExecuteNonQuery();

        //}
        public void Insert<T>(T item)
        {
            var mainObjectName = item.GetType().Name;
            var columnNames = getAllColumns(item.GetType().Name);
            // Get the type of the object being inserted
            Type dataType = item.GetType();

            // Get the properties of the object
            PropertyInfo[] properties = dataType.GetProperties();

            // Loop through the properties
            foreach (PropertyInfo property in properties)
            {

                //if (property.GetValue(item) is IList)
                //{
                //    IList list = property.GetValue(item) as IList;
                //    for (int i = 0; i < list.Count; i++)
                //    {

                //        Insert(list[i]);


                //    }
                //}

                //Type innerClassType = property.PropertyType.GetGenericArguments()[0];
                //var objectList = property.GetValue(item);

                //if (objectList != null)
                //{
                //    var myOrm = Activator.CreateInstance(typeof(MyORM<G, T>).MakeGenericType(innerClassType));

                //    // Get the foreign key property by convention
                //    string navPropName = property.Name;
                //    string fkPropName = navPropName + "Id";
                //    var foreignKeyProperty = innerClassType.GetProperty(fkPropName);

                //    foreach (var element in objectList as IEnumerable)
                //    {
                //        if (foreignKeyProperty != null && foreignKeyProperty.PropertyType == typeof(G))
                //        {
                //            // Set the foreign key value by convention
                //            foreignKeyProperty.SetValue(element, item.GetType().GetProperty("Id").GetValue(item));
                //        }


                //        // Cast nestedData to T before calling Insert method
                //        myOrm.GetType().GetMethod("Insert", new Type[] { innerClassType }).Invoke(myOrm, new object[] { Convert.ChangeType(element, innerClassType) });


                //        //OperationOnNestedObjectAndCollection("Insert",item);

                //    }
                //}



                // Check if the property is a nested object
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string) &&
                    property.GetValue(item) is not IList)
                {
                    // Get the nested object
                    var nestedData = property.GetValue(item);


                    // Check if the nested object is not null
                    if (nestedData != null)
                    {
                        // Check if the nested object has a foreign key property
                        PropertyInfo foreignKeyProperty = nestedData.GetType().GetProperty(dataType.Name + "Id");
                        if (foreignKeyProperty != null)
                        {
                            // Get the value of the foreign key property from the parent object
                            object foreignKeyValue = dataType.GetProperty("Id").GetValue(item);

                            // Set the value of the foreign key property on the nested object
                            foreignKeyProperty.SetValue(nestedData, foreignKeyValue);
                        }

                        // Call this method recursively to insert the nested object
                        Insert(nestedData);

                    }
                }


            }

            var sql = new StringBuilder("Insert into ");
            var type = item.GetType();

            sql.Append(type.Name);
            sql.Append('(');
            foreach (var column in columnNames)
            {

                sql.Append(' ').Append(column).Append(',');

            }
            sql.Remove(sql.Length - 1, 1);

            sql.Append(") values(");
            foreach (var column in columnNames)
            {

                sql.Append('@').Append(column).Append(',');

            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(");");

            var query = sql.ToString();

            //DatabaseConnection.CheckConnectionAndOpen(ref _sqlConnection);
            var command = GetCommand(query);

            foreach (var column in columnNames)
            {
                foreach (var property in properties)
                {
                    if (!IsClassOfCurrentAssemblyOrCollectionOrArray(property) && column == property.Name)
                    {
                        command.Parameters.AddWithValue("@" + column, property.GetValue(item));
                    }
                    if (property.PropertyType.IsClass && property.PropertyType != typeof(string) &&
                        property.PropertyType is not IList && column.Contains(property.Name)
                        )
                    {
                        Type _type = item.GetType();
                        PropertyInfo presentAddressProperty = _type.GetProperty(property.Name);
                        object presentAddressValue = presentAddressProperty.GetValue(item, null);

                        Type presentAddressType = presentAddressValue.GetType();
                        PropertyInfo presentAddressIdProperty = presentAddressType.GetProperty("Id");
                        object presentAddressIdValue = presentAddressIdProperty.GetValue(presentAddressValue, null);


                        command.Parameters.AddWithValue("@" + column, presentAddressIdValue);
                    }
                }

            }
            //foreach (var property in properties)
            //{
            //    if (!IsClassOfCurrentAssemblyOrCollectionOrArray(property))
            //    {
            //        command.Parameters.AddWithValue("@" + property.Name, property.GetValue(item));
            //    }
            //}
            // int id = (int)command.ExecuteScalar();

            command.ExecuteNonQuery();
            /*
            foreach (PropertyInfo property in properties)
            {

                if (property.GetValue(item) is IList)
                {
                    IList list = property.GetValue(item) as IList;
                    for (int i = 0; i < list.Count; i++)
                    {

                        Insert(list[i]);


                    }
                }
            }
            */
            foreach (PropertyInfo property in item.GetType().GetProperties())
            {
                if (property.PropertyType.IsGenericType &&
                    typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    var nestedData = property.GetValue(item);
                    if (nestedData != null)
                    {


                        foreach (var element in nestedData as IEnumerable)
                        {
                            var tableName = element.GetType().Name;
                            var foreignKeyColumn = getAllColumns(tableName).Where(x => x == mainObjectName + "Id").Select(x => x);
                            //                        var foreignKeyField = element.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                            //.FirstOrDefault(f => f.FieldType.IsClass && f.Name.EndsWith(mainObjectName + "Id"));
                            object foreignKeyValue = null;
                            if (foreignKeyColumn != null)
                            {
                                var fkValue = item.GetType().GetProperty("Id").GetValue(item);
                                foreignKeyValue = fkValue;
                                //foreignKeyColumn.SetValue(element, fkValue);
                            }
                            InsertListType(element, foreignKeyValue, item);


                        }

                    }
                }
            }



                }
                private void InsertListType(object element, object fkValue,object foreignTable)
        {
            var tableName = element.GetType().Name;
            var columnNames = getAllColumns(tableName);
            var sql = new StringBuilder("Insert into ");
            sql.Append(tableName).Append('(');
            foreach (var column in columnNames)
            {
                sql.Append(column).Append(',');
            }
            sql.Remove(sql.Length - 1, 1).Append(") values(");
            foreach (var column in columnNames)
            {
                sql.Append('@').Append(column).Append(',');
            }
            sql.Remove(sql.Length - 1, 1).Append(");");
            var query = sql.ToString();

            var command = GetCommand(query);
            bool oneTimeForeignSet = true;
            foreach (var column in columnNames)
            {
                foreach (var property in element.GetType().GetProperties())
                {
                    if (!IsClassOfCurrentAssemblyOrCollectionOrArray(property) && column == property.Name)
                    {
                        command.Parameters.AddWithValue("@" + column, property.GetValue(element));
                    }
                    else if (property.PropertyType.IsClass && property.PropertyType != typeof(string) &&
                             !typeof(IEnumerable).IsAssignableFrom(property.PropertyType) &&
                             column.Contains(property.Name))
                    {
                        Type nestedType = property.PropertyType;
                        PropertyInfo nestedIdProperty = nestedType.GetProperty("Id");
                        object nestedIdValue = nestedIdProperty.GetValue(property.GetValue(element));
                        command.Parameters.AddWithValue("@" + column, nestedIdValue);
                    }
                    else if (column == foreignTable.GetType().Name+"Id" && oneTimeForeignSet)
                    {
                        command.Parameters.AddWithValue("@" + column, fkValue);
                        oneTimeForeignSet = false;
                    }
                }
            }
            command.ExecuteNonQuery();



        }

        private void InsertNestedData<T>(T item)
        {
            foreach (PropertyInfo property in item.GetType().GetProperties())
            {
                if (property.PropertyType.IsGenericType &&
                    typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    var nestedData = property.GetValue(item);
                    if (nestedData != null)
                    {
                        Type innerClassType = property.PropertyType.GetGenericArguments()[0];
                        foreach (var element in nestedData as IEnumerable)
                        {
                            InsertNestedData(element);
                        }
                    }
                }
                else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    var nestedData = property.GetValue(item);
                    if (nestedData != null)
                    {
                        InsertNestedData(nestedData);
                    }
                }
            }

            // Now insert the item itself into the database
            // ...
        }

        void InsertList(object item,object id) {
            var columnNames = getAllColumns(item.GetType().Name);
            // Get the type of the object being inserted
            Type dataType = item.GetType();

            // Get the properties of the object
            PropertyInfo[] properties = dataType.GetProperties();

            // Loop through the properties
            foreach (PropertyInfo property in properties)
            {
                // Check if the property is a nested object
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string) &&
                    property.GetValue(item) is not IList)
                {
                    // Get the nested object
                    var nestedData = property.GetValue(item);


                    // Check if the nested object is not null
                    if (nestedData != null)
                    {
                        // Check if the nested object has a foreign key property
                        PropertyInfo foreignKeyProperty = nestedData.GetType().GetProperty(dataType.Name + "Id");
                        if (foreignKeyProperty != null)
                        {
                            // Get the value of the foreign key property from the parent object
                            object foreignKeyValue = dataType.GetProperty("Id").GetValue(item);

                            // Set the value of the foreign key property on the nested object
                            foreignKeyProperty.SetValue(nestedData, foreignKeyValue);
                        }

                        // Call this method recursively to insert the nested object
                        Insert(nestedData);

                    }
                }


            }

            var sql = new StringBuilder("Insert into ");
            var type = item.GetType();

            sql.Append(type.Name);
            sql.Append('(');
            foreach (var column in columnNames)
            {

                sql.Append(' ').Append(column).Append(',');

            }
            sql.Remove(sql.Length - 1, 1);

            sql.Append(") values(");
            foreach (var column in columnNames)
            {

                sql.Append('@').Append(column).Append(',');

            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(");");

            var query = sql.ToString();

            //DatabaseConnection.CheckConnectionAndOpen(ref _sqlConnection);
            var command = GetCommand(query);

            foreach (var column in columnNames)
            {
                foreach (var property in properties)
                {
                    if (!IsClassOfCurrentAssemblyOrCollectionOrArray(property) && column == property.Name)
                    {
                        command.Parameters.AddWithValue("@" + column, property.GetValue(item));
                    }
                    if (property.PropertyType.IsClass && property.PropertyType != typeof(string) &&
                        property.PropertyType is not IList && column.Contains(property.Name)
                        )
                    {
                        Type _type = item.GetType();
                        PropertyInfo presentAddressProperty = _type.GetProperty(property.Name);
                        object presentAddressValue = presentAddressProperty.GetValue(item, null);

                        Type presentAddressType = presentAddressValue.GetType();
                        PropertyInfo presentAddressIdProperty = presentAddressType.GetProperty("Id");
                        object presentAddressIdValue = presentAddressIdProperty.GetValue(presentAddressValue, null);


                        command.Parameters.AddWithValue("@" + column, presentAddressIdValue);
                    }
                }

            }


            command.ExecuteNonQuery();
        }

        private int GetPrimaryKeyValue(string tableName)
        {
            int primaryKeyValue ;
            string sql = "SELECT MAX(" + tableName + "_id) FROM " + tableName;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = GetCommand(sql);
                 primaryKeyValue = (int)command.ExecuteScalar();
                //primaryKeyValue = (result == DBNull.Value) ? 1 : Convert.ToInt32(result) + 1;
            }
            return primaryKeyValue;
        }




        private void OperationOnNestedObjectAndCollection<T>(string NameOfOperation, T item)
        {
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                Type innerClassType = property.PropertyType;

                if (IsClassOfCurrentAssembly(property))
                {
                    var myOrm = Activator.CreateInstance(typeof(MyORM<G,T>)
                        .MakeGenericType(property.PropertyType));

                    myOrm.GetType().GetMethod(NameOfOperation, new Type[] { innerClassType })
                        .Invoke(myOrm, new object[] { property.GetValue(item) });
                }
                else if (IsCollectionOrArray(property))
                {
                    innerClassType = property.PropertyType.GetGenericArguments()[0];
                    var objectList = property.GetValue(item);

                    var myOrm = Activator.CreateInstance(typeof(MyORM<G,T>)
                            .MakeGenericType(innerClassType));

                    var methodInfo = myOrm.GetType()
                        .GetMethod(NameOfOperation, new Type[] { innerClassType });

                    foreach (var element in objectList as IEnumerable)
                    {
                        methodInfo.Invoke(myOrm, new object[] { element });
                    }
                }
            }
        }

        private string GenerateInsertStatement(object data)
        {
            // Get the type of the object being inserted
            Type dataType = data.GetType();

            // Get the name of the table
            string tableName = dataType.Name;

           
            // Get the properties of the object
            PropertyInfo[] properties = dataType.GetProperties();

            // Generate the SQL insert statement
            StringBuilder insertStatement = new StringBuilder();
            insertStatement.AppendFormat("INSERT INTO {0} (", tableName);

            bool firstColumn = true;
            foreach (PropertyInfo property in properties)
            {

                if (!IsClassOfCurrentAssemblyOrCollectionOrArray(property))
                {

                    if (!firstColumn)
                    {
                        insertStatement.Append(", ");
                    }
                    else
                    {
                        firstColumn = false;
                    }

                    insertStatement.Append(property.Name);
                }
                
            }

            insertStatement.Append(") VALUES (");

            //bool firstValue = true;
            foreach (var property in properties)
            {
                if (!IsClassOfCurrentAssemblyOrCollectionOrArray(property))
                {
                    insertStatement.Append('@').Append(property.Name).Append(',');
                }
            }
            insertStatement.Remove(insertStatement.Length - 1, 1);
            insertStatement.Append(")");

            return insertStatement.ToString();
        }

        private void ExecuteQuery(string command_text)
        {
            using SqlCommand command = GetCommand(command_text);
            //int id = (int)command.ExecuteScalar();
            command.ExecuteNonQuery();
            //return id;
        }
        private SqlCommand GetCommand(string sqlcommadText)// this method will create the connection using sql command and and retrun the command
        {
            //first use _connectionString to create SqlConnection
            SqlConnection sqlConnection = new SqlConnection(this._connectionString);

            SqlCommand command = new SqlCommand();

            command.Connection = sqlConnection;//command connection --> sql connection 
            command.CommandText = sqlcommadText;// command text --> sql command text inserted

            if (sqlConnection.State != System.Data.ConnectionState.Open)
                sqlConnection.Open();

            return command;

        }

        private List<string> getAllColumns(string tableName)
        {
            List<string> columns = new List<string>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string columnName = reader.GetString(0);
                    // Do something with the column name
                    columns.Add(columnName);
                }
                reader.Close();
            }
            return columns;
        }

        public void Update(T item)
        {
           
        }

        public void Delete(T item)
        {
           
        }

        public void Delete(G id)
        {
          
        }

        public T GetById(G id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = $"SELECT * FROM {typeof(T).Name}s WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var item = Activator.CreateInstance<T>();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if (property.Name != "Id")
                        {
                            var value = reader[property.Name];
                            if (value != DBNull.Value)
                            {
                                property.SetValue(item, value);
                            }
                        }
                        else
                        {
                            property.SetValue(item, id);
                        }
                    }
                    return item;
                }
                else
                {
                    return default(T);
                }
            }
        }




        public List<T> GetAll()
        {
            List<Dictionary<string, object>> fullTableData = getFullTabledata();
            var allDatasAfterConverting_to_Generic = ConvertToTList<T>(fullTableData);
            return allDatasAfterConverting_to_Generic;
        }
        public static List<T> ConvertToTList<T>(List<Dictionary<string, object>> dictionaryList)
        {
            List<T> tList = new List<T>();
            Type tType = typeof(T);

            foreach (Dictionary<string, object> dictionary in dictionaryList)
            {
                T tObject = (T)Activator.CreateInstance(tType);

                foreach (PropertyInfo propertyInfo in tType.GetProperties())
                {
                    if (dictionary.ContainsKey(propertyInfo.Name))
                    {
                        object propertyValue = dictionary[propertyInfo.Name];
                        propertyInfo.SetValue(tObject, propertyValue);
                    }
                }

                tList.Add(tObject);
            }

            return tList;
        }

        private List<Dictionary<string, object>> getFullTabledata()
        {         
            string showFullTableQuery = $"select * from {_tableName}";
            List<Dictionary<string, object>> whole_table = new List<Dictionary<string, object>>();
            using SqlCommand command = GetCommand(showFullTableQuery);

            SqlDataReader reader = command.ExecuteReader();
            //each row wise column and their values are added to result list
            while (reader.Read())
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                // every column along the value of that row is added to dict
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dict.Add(reader.GetName(i), reader.GetValue(i));
                }
                whole_table.Add(dict);
            }
            
            return whole_table;

        }
    }

}

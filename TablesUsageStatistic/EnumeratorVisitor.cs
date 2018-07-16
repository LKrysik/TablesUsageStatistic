using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace TablesUsageStatistic
{
    public class EnumeratorVisitor : TSqlFragmentVisitor
    {
        public List<QueryExpression> Nodes = new List<QueryExpression>();


        public override void Visit(QueryExpression node)
        {

            base.Visit(node);

            if(!Nodes.Any(p=>p.StartOffset <= node.StartOffset && p.StartOffset + p.FragmentLength >= node.StartOffset + node.FragmentLength))
                Nodes.Add(node);

        }
    }
    public class NamedTableVisitor : TSqlFragmentVisitor
    {
        public List<NamedTableReference> Nodes = new List<NamedTableReference>();

        public override void ExplicitVisit(NamedTableReference node)
        {
            //var querySpecification = (node.QueryExpression) as QuerySpecification;
            //if()
            Nodes.Add(node);
        }
    }
    public class Table
    {
        public string Name;
        public string Schema = "";
        public string Database = "";
        public string Server = "";

        public int iterator = 1;

        public override string ToString()
        {
            return Server + " " + Database + " " + Schema + "." + Name + " : (" + iterator.ToString() +")";
        }
        //public bool Equals(Table obj)
        //{
        //    if (obj == null) return false;
        //    return Name == ((Table)obj)?.Name && Schema == ((Table)obj)?.Schema && Database == ((Table)obj)?.Database && Server == ((Table)obj)?.Server;
        //}
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if(this.GetHashCode() == obj.GetHashCode())
            {
                iterator = iterator + ((Table)obj).iterator;
 
                return true;
            }
            else
            {
                return false;
            }
    
        }
        public override int GetHashCode()
        {
            MD5 md5Hasher = MD5.Create();
            var hashed = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(Server + " . " + Database + " . " + Schema + " . " + Name));
            var ivalue = BitConverter.ToInt32(hashed, 0);
            return ivalue;
        }

    }
    public class StatsVisitor : TSqlFragmentVisitor
    {
        public List<Table> Nodes = new List<Table>();
        public List<Table> DistinctNodes = new List<Table>();
        public override void ExplicitVisit(NamedTableReference node)
        {
            //var querySpecification = (node.QueryExpression) as QuerySpecification;

            Table table = new Table();          
            table.Schema = node?.SchemaObject?.SchemaIdentifier?.Value;
            table.Name = node?.SchemaObject?.BaseIdentifier?.Value;
            table.Database = node?.SchemaObject?.DatabaseIdentifier?.Value;
            table.Server = node?.SchemaObject?.ServerIdentifier?.Value;
             
            Nodes.Add(table);
        }
        public IEnumerable<Table> GetDistinctNodes()
        {
            //DistinctNodes =
            return Nodes.Distinct();
        }
    }

}

using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager.Builders
{
    public class Context
    {
        public QueryContainer mainQuery;

        public QueryContainer mainFilter;
    }

    public abstract class Op
    {
        public QueryContainer Query { get; set; }

        public QueryContainer Filter { get; set; }

        public abstract void ParseQuery(Context ctx);

        public abstract void ParseFilter(Context ctx);
    }

    public class AndOp : Op
    {
        public override void ParseQuery(Context ctx)
        {
            ctx.mainQuery &= Query;
        }

        public override void ParseFilter(Context ctx)
        {
            ctx.mainFilter &= Filter;
        }
    }

    public class OrOp : Op
    {

        public override void ParseQuery(Context ctx)
        {
            ctx.mainQuery |= this.Query;
        }

        public override void ParseFilter(Context ctx)
        {
            ctx.mainFilter |= Filter;
        }
    }

    public class NotOp : Op
    {

        public override void ParseQuery(Context ctx)
        {
            ctx.mainQuery = !ctx.mainQuery;
        }

        public override void ParseFilter(Context ctx)
        {
            ctx.mainFilter = !ctx.mainFilter;
        }
    }
}

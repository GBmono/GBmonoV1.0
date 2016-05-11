using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager.Builders
{
    public class QueryBuilder
    {
        private List<Op> _opList = new List<Op>();
        private Context _context = null;

        public bool _includeEquality = true;

        public QueryBuilder IsIncludeEquality(bool include)
        {
            _includeEquality = include;
            return this;
        }

        public void AddQuery(QueryContainer container, bool andOp, bool not = false)
        {
            if (_context == null)
            {
                _context = new Context() { mainQuery = container };
                if (not)
                {
                    _context.mainQuery = !_context.mainQuery;
                }
            }
            else
            {
                if (andOp)
                {
                    _opList.Add(new AndOp()
                    {
                        Query = not ? !container : container
                    });

                }
                else
                {
                    _opList.Add(new OrOp()
                    {
                        Query = not ? !container : container
                    });
                }
            }
        }

        public QueryBuilder AddWildcard<T>(string fieldName, T fieldValue, bool andOp, bool not = false)
        {
            if (string.IsNullOrWhiteSpace(fieldName) || fieldValue == null)
            {
                return this;
            }

            var wq = new WildcardQuery() { Field = fieldName, Value = fieldValue };

            if (_context == null)
            {
                _context = new Context() { mainQuery = new QueryContainer(wq) };
                if (not)
                {
                    _context.mainQuery = !_context.mainQuery;
                }
            }
            else
            {
                if (andOp)
                {
                    _opList.Add(new AndOp()
                    {
                        Query = not ? !new QueryContainer(wq) : new QueryContainer(wq)
                    });

                }
                else
                {
                    _opList.Add(new OrOp()
                    {
                        Query = not ? !new QueryContainer(wq) : new QueryContainer(wq)
                    });
                }
            }

            return this;
        }

        public QueryBuilder AndWildCard<T>(string fieldName, T fieldValue)
        {
            return AddWildcard(fieldName, fieldValue, true);
        }

        public QueryBuilder NotWildCard<T>(string fieldName, T fieldValue)
        {
            return AddWildcard(fieldName, fieldValue, false, true);
        }

        public QueryBuilder OrWildCard<T>(string fieldName, T fieldValue)
        {
            return AddWildcard(fieldName, fieldValue, false);
        }

        #region Terms OP

        public QueryBuilder AddTerms<T>(string fieldName, IEnumerable<T> values, bool andOp, bool not = false)
        {
            foreach (var val in values)
            {
                AddTerm(fieldName, val, andOp, not);
            }

            return this;
        }

        public QueryBuilder AddTerm<T>(string fieldName, T fieldValue, bool andOp, bool not = false)
        {
            if (string.IsNullOrWhiteSpace(fieldName) || fieldValue == null)
            {
                return this;
            }

            var term = new TermQuery() { Field = fieldName, Value = fieldValue };

            if (_context == null)
            {
                _context = new Context() { mainQuery = new QueryContainer(term) };
                if (not)
                {
                    _context.mainQuery = !_context.mainQuery;
                }
            }
            else
            {
                if (andOp)
                {
                    _opList.Add(new AndOp()
                    {
                        Query = not ? !new QueryContainer(term) : new QueryContainer(term)
                    });

                }
                else
                {
                    _opList.Add(new OrOp()
                    {
                        Query = not ? !new QueryContainer(term) : new QueryContainer(term)
                    });
                }
            }

            return this;
        }

        public QueryBuilder AndTerm<T>(string fieldName, T fieldValue)
        {
            return AddTerm(fieldName, fieldValue, true);
        }

        public QueryBuilder OrTerm<T>(string fieldName, T fieldValue)
        {
            return AddTerm(fieldName, fieldValue, false);
        }

        #endregion

        #region Range OP
        public QueryBuilder AddRange<T>(string fieldName, T max, T min, bool andOp, bool not = false)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                return this;
            }

            if (max == null && min == null)
            {
                return this;
            }

            var range = new TermRangeQuery() { Field = fieldName };
            if (max != null)
            {
                if (_includeEquality)
                    range.LessThanOrEqualTo = max.ToString();
                else
                    range.LessThan = max.ToString();

            }
            if (min != null)
            {
                if (_includeEquality)
                    range.GreaterThanOrEqualTo = min.ToString();
                else
                    range.GreaterThan = min.ToString();
            }

            if (_context == null)
            {
                _context = new Context() { mainQuery = new QueryContainer(range) };
                if (not)
                {
                    _context.mainQuery = !_context.mainQuery;
                }
            }
            else
            {
                if (andOp)
                {
                    _opList.Add(new AndOp()
                    {
                        Query = not ? !new QueryContainer(range) : new QueryContainer(range)
                    });

                }
                else
                {
                    _opList.Add(new OrOp()
                    {
                        Query = not ? !new QueryContainer(range) : new QueryContainer(range)
                    });
                }
            }

            return this;
        }

        public QueryBuilder AndRange<T>(string fieldName, T max, T min)
        {
            return AddRange(fieldName, max, min, true);
        }
        public QueryBuilder OrRange<T>(string fieldName, T max, T min)
        {
            return AddRange(fieldName, max, min, false);
        }

        #endregion


        private QueryBuilder AddMatch<T>(string fieldName, T fieldValue, bool andOp, bool not = false)
        {
            if (string.IsNullOrWhiteSpace(fieldName) || fieldValue == null)
            {
                return this;
            }

            var mq = new MatchQuery() { Field = fieldName, Query = fieldValue.ToString() };

            if (_context == null)
            {
                _context = new Context() { mainQuery = new QueryContainer(mq) };
                if (not)
                {
                    _context.mainQuery = !_context.mainQuery;
                }
            }
            else
            {
                if (andOp)
                {
                    _opList.Add(new AndOp()
                    {
                        Query = not ? !new QueryContainer(mq) : new QueryContainer(mq)
                    });

                }
                else
                {
                    _opList.Add(new OrOp()
                    {
                        Query = not ? !new QueryContainer(mq) : new QueryContainer(mq)
                    });
                }
            }

            return this;
        }

        public QueryBuilder AndMatch<T>(string fieldName, T fieldValue)
        {
            return AddMatch(fieldName, fieldValue, true);
        }

        public QueryBuilder OrMatch<T>(string fieldName, T fieldValue)
        {
            return AddMatch(fieldName, fieldValue, false);
        }

        private QueryBuilder AddMultiMatch<T>(string[] fieldNames, T fieldValue, bool andOp, bool not = false)
        {
            if (fieldNames == null || fieldNames.Length == 0 || fieldValue == null)
            {
                return this;
            }

            var mq = new MultiMatchQuery() { Fields = fieldNames, Query = fieldValue.ToString() };

            if (_context == null)
            {
                _context = new Context() { mainQuery = new QueryContainer(mq) };
                if (not)
                {
                    _context.mainQuery = !_context.mainQuery;
                }
            }
            else
            {
                if (andOp)
                {
                    _opList.Add(new AndOp()
                    {
                        Query = not ? !new QueryContainer(mq) : new QueryContainer(mq)
                    });

                }
                else
                {
                    _opList.Add(new OrOp()
                    {
                        Query = not ? !new QueryContainer(mq) : new QueryContainer(mq)
                    });
                }
            }

            return this;
        }

        public QueryBuilder AndMultiMatch<T>(string[] fieldNames, T fieldValue)
        {
            return AddMultiMatch(fieldNames, fieldValue, true);
        }

        public QueryBuilder OrMultiMatch<T>(string[] fieldNames, T fieldValue)
        {
            return AddMultiMatch(fieldNames, fieldValue, false);
        }

        public QueryBuilder Not()
        {
            if (_opList.Any())
            {
                _opList.Add(new NotOp());
            }
            return this;
        }

        public QueryContainer Build()
        {

            foreach (var op in _opList)
            {
                op.ParseQuery(_context);
            }

            return _context.mainQuery;
        }

    }
}

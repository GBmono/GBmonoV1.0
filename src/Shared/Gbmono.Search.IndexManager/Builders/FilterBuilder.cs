using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager.Builders
{
    public class FilterBuilder
    {
        private List<Op> _opList = new List<Op>();
        private Context _context = null;
        public bool _includeEquality = true;

        public FilterBuilder IsIncludeEquality(bool include)
        {
            _includeEquality = include;
            return this;
        }

        public FilterBuilder AddFilter(QueryContainer container, bool andOp, bool not = false)
        {
            //if (_context == null)
            //{
            //    _context = new Context() { mainFilter = container };
            //    if (not)
            //    {
            //        _context.mainFilter = !_context.mainFilter;
            //    }
            //}
            //else
            //{
            //    if (andOp)
            //    {
            //        _opList.Add(new AndOp()
            //        {
            //            Filter = not ? !container : container
            //        });

            //    }
            //    else
            //    {
            //        _opList.Add(new OrOp()
            //        {
            //            Filter = not ? !container : container
            //        });
            //    }
            //}


            if (container == null)
            {
                return this;
            }

            if (_context == null)
            {
                _context = new Context() { mainFilter = container };
                if (not)
                {
                    _context.mainFilter = !_context.mainFilter;
                }
            }
            else
            {
                if (andOp)
                {
                    _opList.Add(new AndOp()
                    {
                        Filter = not ? !container : container
                    });

                }
                else
                {
                    _opList.Add(new OrOp()
                    {
                        Filter = not ? !container : container
                    });
                }
            }

            return this;

        }

        public FilterBuilder AddTerms<T>(string fieldName, IEnumerable<T> values, bool andOp, bool not = false)
        {
            foreach (var val in values)
            {
                AddTerm(fieldName, val, andOp, not);
            }

            return this;
        }


        public FilterBuilder AddTerm<T>(string fieldName, T fieldValue, bool andOp, bool not = false)
        {
            if (string.IsNullOrWhiteSpace(fieldName) || fieldValue == null)
            {
                return this;
            }

            var term = new TermQuery() { Field = fieldName, Value = fieldValue };

            AddFilter(new QueryContainer(term), andOp, not);
            //if (_context == null)
            //{
            //    _context = new Context() { mainFilter = new FilterContainer(term) };
            //    if (not)
            //    {
            //        _context.mainFilter = !_context.mainFilter;
            //    }
            //}
            //else
            //{
            //    if (andOp)
            //    {
            //        _opList.Add(new AndOp()
            //        {
            //            Filter = not ? !new FilterContainer(term) : new FilterContainer(term)
            //        });

            //    }
            //    else
            //    {
            //        _opList.Add(new OrOp()
            //        {
            //            Filter = not ? !new FilterContainer(term) : new FilterContainer(term)
            //        });
            //    }
            //}

            return this;
        }

        public FilterBuilder AndTerm<T>(string fieldName, T fieldValue)
        {
            return AddTerm(fieldName, fieldValue, true);
        }
        public FilterBuilder OrTerm<T>(string fieldName, T fieldValue)
        {
            return AddTerm(fieldName, fieldValue, false);
        }

        public FilterBuilder AddRange<T>(string fieldName, T max, T min, bool andOp, bool not = false)
        {
            if (max == null && min == null)
            {
                return this;
            }
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                return this;
            }



            var range = new TermRangeQuery() { Field = fieldName };//RangeFilter() { Field = fieldName };
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

            AddFilter(new QueryContainer(range), andOp, not);

            return this;
        }

        public FilterBuilder AndRange<T>(string fieldName, T max, T min)
        {
            return AddRange(fieldName, max, min, true);
        }

        public FilterBuilder OrRange<T>(string fieldName, T max, T min)
        {
            return AddRange(fieldName, max, min, false);
        }

        public FilterBuilder Not()
        {
            if (_opList.Any())
            {
                _opList.Add(new NotOp());
            }

            return this;
        }

        public FilterBuilder AddGeoBoundBox(string fieldName, ViewModel.GeoPoint topLeft, ViewModel.GeoPoint bottomRight, bool andOp, bool not = false)
        {

            if (topLeft == null && bottomRight == null)
            {
                return this;
            }
            if (string.IsNullOrWhiteSpace(fieldName))
            {
                return this;
            }



            var box = new GeoBoundingBoxQuery() { Field = fieldName };
            box.BoundingBox = new BoundingBox() { TopLeft = new GeoLocation(topLeft.Lat,topLeft.Lon), BottomRight = new GeoLocation(bottomRight.Lat,bottomRight.Lon) };

            AddFilter(new QueryContainer(box), andOp, not);

            return this;
        }

        public FilterBuilder AndGeoBoundBox(string fieldName, ViewModel.GeoPoint topLeft, ViewModel.GeoPoint bottonRight)
        {
            return AddGeoBoundBox(fieldName, topLeft, bottonRight, true);
        }
        public FilterBuilder OrGeoBoundBox(string fieldName, ViewModel.GeoPoint topLeft, ViewModel.GeoPoint bottonRight)
        {
            return AddGeoBoundBox(fieldName, topLeft, bottonRight, false);
        }
        public FilterBuilder NotGeoBoundBox(string fieldName, ViewModel.GeoPoint topLeft, ViewModel.GeoPoint bottonRight)
        {
            return AddGeoBoundBox(fieldName, topLeft, bottonRight, false, true);
        }

        public QueryContainer Build()
        {
            if (_context != null)
            {
                foreach (var op in _opList)
                {
                    op.ParseFilter(_context);
                }

                return _context.mainFilter;
            }
            else
            {
                return null;
            }
        }
    }
}

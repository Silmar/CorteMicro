/*
This file is part of Cut Micro.

Cut Micro is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

Cut Micro is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with Cut Micro. If not, see http://www.gnu.org/licenses/.
*/
using System.Collections.Generic;

namespace CPanel
{
    public enum SortType
    {
        ByHeight,
        ByWidth,
        ById
    };

    public sealed class sortByHeigth : IComparer<Detail>
    {
        #region IComparer<Detail> Members

        public int Compare(Detail obj1, Detail obj2)
        {
            return obj1.Height - obj2.Height;
        }

        #endregion
    }

    public sealed class sortByWidth : IComparer<Detail>
    {
        #region IComparer<Detail> Members

        public int Compare(Detail obj1, Detail obj2)
        {
            return obj1.Width - obj2.Width;
        }

        #endregion
    }

    public sealed class sortByID : IComparer<Detail>
    {
        #region IComparer<Detail> Members

        public int Compare(Detail obj1, Detail obj2)
        {
            return  (obj1.detailID.CompareTo(obj2.detailID));
        }

        #endregion
    }

    public sealed class sortDTByID : IComparer<DT>
    {
        #region IComparer<DT> Members

        public int Compare(DT obj1, DT obj2)
        {
            return obj1._DetID.CompareTo(obj2._DetID);
        }

        #endregion
    }

    public sealed class sortDetailByY : IComparer<Detail>
    {
        #region IComparer<Detail> Members

        public int Compare(Detail obj1, Detail obj2)
        {
            return obj1.Y.CompareTo(obj2.Y);
        }

        #endregion
    }

    public static class AlternativeSort
    {
        public static void SortDetailsByHeight(List<Detail> Dlist)
        {
            Dlist.Sort(new sortByHeigth());
            List<Detail> OutList = new List<Detail>();
            List<Detail> TmpList = new List<Detail>();
                        
            for (int i = 0; i < Dlist.Count; i++)
            {
                if (i != 0 && Dlist[i].Height != Dlist[i-1].Height)
                {
                    TmpList.Sort(new sortByWidth());
                    OutList.AddRange(TmpList);
                    TmpList.Clear();
                    TmpList.Add(Dlist[i]);
                }
                else
                {
                    TmpList.Add(Dlist[i]);
                }
            }
            if (TmpList.Count > 0)
            {
                TmpList.Sort(new sortByWidth());
                OutList.AddRange(TmpList);
            }

            Dlist.Clear();
            Dlist.AddRange(OutList);
        }

        public static void SortDetailsByWidth(List<Detail> Dlist)
        {
            Dlist.Sort(new sortByWidth());
            List<Detail> OutList = new List<Detail>();
            List<Detail> TmpList = new List<Detail>();

            for (int i = 0; i < Dlist.Count; i++)
            {
                if (i != 0 && Dlist[i].Width != Dlist[i - 1].Width)
                {
                    TmpList.Sort(new sortByHeigth());
                    OutList.AddRange(TmpList);
                    TmpList.Clear();
                    TmpList.Add(Dlist[i]);
                }
                else
                {
                    TmpList.Add(Dlist[i]);
                }
            }
            if (TmpList.Count > 0)
            {
                TmpList.Sort(new sortByHeigth());
                OutList.AddRange(TmpList);
            }

            Dlist.Clear();
            Dlist.AddRange(OutList);
        }

        public static void SortDetailsByID(List<Detail> Dlist)
        {
            Dlist.Sort(new sortByID());
            List<Detail> OutList = new List<Detail>();
            List<Detail> TmpList = new List<Detail>();

            for (int i = 0; i < Dlist.Count; i++)
            {
                if (i != 0 && Dlist[i].detailID != Dlist[i - 1].detailID)
                {
                    TmpList.Sort(new sortByHeigth());
                    OutList.AddRange(TmpList);
                    TmpList.Clear();
                    TmpList.Add(Dlist[i]);
                }
                else
                {
                    TmpList.Add(Dlist[i]);
                }
            }
            if (TmpList.Count > 0)
            {
                TmpList.Sort(new sortByHeigth());
                OutList.AddRange(TmpList);
            }

            Dlist.Clear();
            Dlist.AddRange(OutList);
        }
    }



}
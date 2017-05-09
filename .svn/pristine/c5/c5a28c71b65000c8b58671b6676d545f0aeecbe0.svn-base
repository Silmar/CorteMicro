using System;
using System.Collections.Generic;
using System.Text;

namespace CPanel
{
    public class AutoPlacer
    {
        private static bool IsFreeArea(Detail d)
        {
            return d.isFreeArea;
        }
        private Predicate<Detail> faPredicate = IsFreeArea;

        private void AutoPlaceHM(List<CutPanel> Plist)
        {
            //clear freareas, reset all details
            foreach (CutPanel P in Plist)
            {
                P.PlacedDetails.RemoveAll(faPredicate);
                if (P.PanelId != 0)
                {
                    foreach (Detail D in P.PlacedDetails)
                    {
                        Plist[0].PlacedDetails.Add(D);
                    }
                    P.PlacedDetails.Clear();
                }
            }

            //Start autoplace
            foreach (CutPanel P in Plist)
            {
                if (P.PanelId != 0)
                {
                    ProcessPanel(P, Plist[0].PlacedDetails);
                }
            }
        }

        public void AutoPlaceHorizontalMethod(List<CutPanel> Plist)
        {
           // for (int i = 10; i > 0; i--)
          //  {
                AutoPlaceHM(Plist);
          //  }


            //Redraw
            {
                foreach (CutPanel P in Plist)

                    P.Redraw();
            }
        }

        /// <summary>
        /// Finds largest detail that fits into space
        /// </summary>
        /// <param name="maxHeight"></param>
        /// <param name="maxWidth"></param>
        /// <param name="Dlist"></param>
        /// <returns></returns>
        private Detail FindLargestDetail(int maxHeight, int maxWidth, List<Detail> Dlist)
        {
            bool maxrotated = false;
            Detail max = new Detail(0, 0, 0, 0); ;
            foreach (Detail D in Dlist)
            {
                if (D.Width <= maxWidth && D.Height <= maxHeight && D.Width * D.Height > max.Height * max.Width)
                {
                    if (maxrotated)
                    {
                        maxrotated = false;
                        max.Rotate();
                    }
                    max = D;
                }
                else if (D.Height <= maxWidth && D.Width <= maxHeight && D.Width * D.Height > max.Height * max.Width && D._CanBeRotated)
                {
                    if (maxrotated)
                    {
                        max.Rotate();
                    }
                    D.Rotate();
                    max = D;
                    maxrotated = true;
                }
            }

            if (max.RealHeight * max.RealWidth == 0)
            {
                throw new Exception();
            }

            return max;
        }

        /// <summary>
        /// Returns list of details to place vertically
        /// </summary>
        /// <param name="reqHeight"></param>
        /// <param name="maxWidth"></param>
        /// <param name="Dlist"></param>
        /// <returns></returns>
        private List<Detail> FindFittingDetails(int reqHeight, int maxWidth, List<Detail> Dlist)
        {
            List<List<Detail>> fDetails = new List<List<Detail>>();            /* Detail largest;
             try //First see if there are any one detail that can fit itself
             {
                 largest = FindLargestDetail(reqHeight, maxWidth, Dlist);
                 if (largest.Height == reqHeight)
                 {
                     fDetails.Add(largest);
                     return fDetails;
                 }
             }
             catch { }; */
            List<List<Detail>> sortedDetails = new List<List<Detail>>();

            //Get lists with details sorted by width
            Dlist.Sort(new sortByWidth());
            List<Detail> tmplist = new List<Detail>();
            int lastwidth = 0;
           // Detail lastdetail = null;
            foreach (Detail D in Dlist)
            {
               // lastdetail = D;
                if (D.Width != lastwidth)
                {
                    lastwidth = D.Width;
                    if (tmplist.Count > 0)
                    {
                        fDetails.Add(tmplist);
                        tmplist = new List<Detail>();
                    }
                    tmplist.Add(D);
                }
                else
                    tmplist.Add(D);
            }

            if(tmplist.Count >0)
                fDetails.Add(tmplist);

            //Find best collections of details that fits in (reqHeight, maxWidth)
            foreach (List<Detail> D in fDetails)
            {
                if (D[0].Width > maxWidth)
                    D.Clear();
                else
                {
                    try
                    {
                        sortedDetails.Add(findBestFitByHeight(reqHeight, D));
                    }
                    catch { };
                }
            }

            fDetails.Clear();
            if (sortedDetails.Count == 0)
                throw new Exception();

            //find maximum height sum
            int maxSumId = -1;
            int maxSum = 0;
            for (int i = 0; i < sortedDetails.Count; i++)
            {
                int currentSum = 0;
                foreach (Detail T in sortedDetails[i])
                    currentSum += T.Height;

                if (currentSum >= maxSum)
                {
                    maxSum = currentSum;
                    maxSumId = i;
                }
            }

            return sortedDetails[maxSumId];


            // return fDetails;
        }

        /// <summary>
        /// Find details that best fittnig into defined height
        /// </summary>
        /// <param name="maxHeight"></param>
        /// <param name="Dlist"></param>
        private List<Detail> findBestFitByHeight(int maxHeight, List<Detail> Dlist)
        {
            //Damn shit i cannot into such matematics...
            List<Detail> outList = new List<Detail>();
            Dlist.Sort(new sortByHeigth());
            List<int> uplist = new List<int>();
            List<int> downlist = new List<int>();

            int usum = 0;
            for (int i = 0; i < Dlist.Count; i++)
            {
                if (usum + Dlist[i].Height <= maxHeight)
                {
                    usum += Dlist[i].Height;
                    uplist.Add(i);
                }
            }

            int dsum = 0;
            for (int i = Dlist.Count - 1; i >= 0; i--)
            {
                if (dsum + Dlist[i].Height <= maxHeight)
                {
                    dsum += Dlist[i].Height;
                    downlist.Add(i);
                }
            }

            if (usum >= dsum)
            {
                foreach (int i in uplist)
                    outList.Add(Dlist[i]);
            }
            else
            {
                foreach (int i in downlist)
                    outList.Add(Dlist[i]);
            }

            if (outList.Count == 0)
                throw new Exception();


            return outList;
        }

        private void ProcessPanel(CutPanel cpanel, List<Detail> Dlist)
        {
            int Yline = 0;
            int Xline = 0;
            try
            {
                while (true)
                {
                   
                    //Start new row
                    Detail dtmp = FindLargestDetail(cpanel.PboardHeight - cpanel.PCutUp - cpanel.PCutDown - Yline,
                        cpanel.PboardWidth - cpanel.PCutLeft - cpanel.PCutRight, Dlist);
                    Dlist.Remove(dtmp);
                    dtmp.X = 0; Xline = dtmp.Width;
                    dtmp.Y = Yline; Yline += dtmp.Height;
                    cpanel.PlacedDetails.Add(dtmp);
                    //Continue row
                    try
                    {
                        while (true)
                        {
                            List<Detail> dtmplist = FindFittingDetails(dtmp.Height,
                                cpanel.PboardWidth - cpanel.PCutLeft - cpanel.PCutRight - Xline, Dlist);
                            int currentY = Yline - dtmp.Height;
                            foreach (Detail D in dtmplist)
                            {
                                D.X = Xline;
                                D.Y = currentY; currentY += D.Height;
                                Dlist.Remove(D);
                                cpanel.PlacedDetails.Add(D);
                            }
                            Xline += dtmplist[0].Width;
                        }
                    }
                    catch { };
                }
            }
            catch { };
        }
    }
}

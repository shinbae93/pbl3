﻿using PBL.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL.BLL
{
    internal class QLDG_BLL
    {
        private static QLDG_BLL _Instance;

        public static QLDG_BLL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new QLDG_BLL();
                }
                return _Instance;
            }
            private set { }
        }

        private QLDG_BLL()
        {
        }

        public List<DocGia> GetListDG(string MaSo, string Name)
        {
            DHP_07Entities db = new DHP_07Entities();
            var l1 = db.DocGias
                .Where(p => p.MSSV.Contains(MaSo) && p.HoTen.Contains(Name))
                .Select(p => p).ToList();
            return l1;
        }

        public DocGia GetDGByMSSV(string mssv)
        {
            DHP_07Entities db = new DHP_07Entities();
            var l1 = db.DocGias
                .Single(p => p.MSSV == mssv);
            return l1;
        }

        public DocGia GetDGByMaDG(int MaDG)
        {
            DHP_07Entities db = new DHP_07Entities();
            var l1 = db.DocGias
                .Single(p => p.MaDocGia == MaDG);
            return l1;
        }

        public void AddDG(DocGia d)
        {
            using (DHP_07Entities db = new DHP_07Entities())
            {
                db.DocGias.Add(d);
                db.SaveChanges();
            }
        }

        public void EditDG(DocGia d)
        {
            using (DHP_07Entities db = new DHP_07Entities())
            {
                var l1 = db.DocGias
                    .Single(p => p.MSSV == d.MSSV);
                l1.HoTen = d.HoTen;
                l1.NgaySinh = d.NgaySinh;
                l1.GioiTinh = d.GioiTinh;
                l1.MaLop = d.MaLop;
                l1.NgayDK = d.NgayDK;
                db.SaveChanges();
            }
        }

        public void DelDG(DocGia d)
        {
            using (DHP_07Entities db = new DHP_07Entities())
            {
                var l1 = db.DocGias
                    .Single(p => p.MSSV == d.MSSV);
                db.DocGias.Remove(l1);
                db.SaveChanges();
            }
        }

        public List<DocGia> SortDG(string m, string tg)
        {
            DHP_07Entities db = new DHP_07Entities();
            if (m == "MaDocGia")
            {
                if (tg == "Giam")
                {
                    var l1 = from p in db.DocGias
                             orderby p.MaDocGia descending
                             select p;
                    return l1.ToList();
                }
                else
                {
                    var l1 = from p in db.DocGias
                             orderby p.MaDocGia ascending
                             select p;
                    return l1.ToList();
                }
            }
            else if (m == "MSSV")
            {
                if (tg == "Giam")
                {
                    var l1 = from p in db.DocGias
                             orderby p.MSSV descending
                             select p;
                    return l1.ToList();
                }
                else
                {
                    var l1 = from p in db.DocGias
                             orderby p.MSSV ascending
                             select p;
                    return l1.ToList();
                }
            }
            else if (m == "HoTen")
            {
                if (tg == "Giam")
                {
                    var l1 = from p in db.DocGias
                             orderby p.HoTen descending
                             select p;
                    return l1.ToList();
                }
                else
                {
                    var l1 = from p in db.DocGias
                             orderby p.HoTen ascending
                             select p;
                    return l1.ToList();
                }
            }
            else if (m == "NgaySinh")
            {
                if (tg == "Giam")
                {
                    var l1 = from p in db.DocGias
                             orderby p.NgaySinh descending
                             select p;
                    return l1.ToList();
                }
                else
                {
                    var l1 = from p in db.DocGias
                             orderby p.NgaySinh ascending
                             select p;
                    return l1.ToList();
                }
            }
            else if (m == "MaLop")
            {
                if (tg == "Giam")
                {
                    var l1 = from p in db.DocGias
                             orderby p.MaLop descending
                             select p;
                    return l1.ToList();
                }
                else
                {
                    var l1 = from p in db.DocGias
                             orderby p.MaLop ascending
                             select p;
                    return l1.ToList();
                }
            }
            else if (m == "NgayDK")
            {
                if (tg == "Giam")
                {
                    var l1 = from p in db.DocGias
                             orderby p.NgayDK descending
                             select p;
                    return l1.ToList();
                }
                else
                {
                    var l1 = from p in db.DocGias
                             orderby p.NgayDK ascending
                             select p;
                    return l1.ToList();
                }
            }
            return null;
        }
    }
}
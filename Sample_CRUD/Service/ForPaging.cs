using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample_CRUD.Service
{
    public class ForPaging
    {
        public int NowPage { get;set;}

        public int MaxPage { get;set;}
        //分頁項目個數，在此設為唯讀
        //以後要修個數，只需要修改這裡而不需要修改其他地方
        public int ItemNum
        {
            get
            {
                return 5;
            }
        }
        public ForPaging()
        {
            this.NowPage=1;
        }
        public ForPaging(int Page)
        {
            this.NowPage = Page;

        }
        
        //設定正確頁數的方法,以避免傳不正確正值
        public void SetRrightPage()
        {
            //判斷當前頁面是否小於1
            if (this.NowPage<1)
            {
                //將頁面設回1;
                this.NowPage=1;
            }
            else if (this.NowPage > this.MaxPage)
            {
                //設定當前頁面為總頁數
                this.NowPage = this.MaxPage;
            }
            if (this.MaxPage.Equals(0))
            {
                this.NowPage=1;

            }

        }


    }
}
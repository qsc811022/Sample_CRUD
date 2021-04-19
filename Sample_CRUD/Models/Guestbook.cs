using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sample_CRUD.Models
{
    public class Guestbook
    {
        //編號
        [DisplayName("編號")]
        public int Id { get;set;}


        [DisplayName("名子")]
        [Required(ErrorMessage="請輸入名子")]
        //名子
        public string Name { get;set;}

        [DisplayName("留言內容:")]
        [Required(ErrorMessage ="請輸入留言內容")]
        [StringLength(100,ErrorMessage ="留言內容不可以超過100字元")]

        //留言內容
        public string Content { get;set;}

        [DisplayName("新增時間:")]

        //新增時間
        public DateTime CreateTime { get;set;}

        [DisplayName("回覆內容")]
        [StringLength(100,ErrorMessage ="回覆內容不可以超過100字元")]
        //回覆內容
        public string Reply { get;set;}


        [DisplayName("回覆時間")]
        //回覆時間  DateTime?=null 
        public DateTime? ReplyTime { get;set;}



    }
}
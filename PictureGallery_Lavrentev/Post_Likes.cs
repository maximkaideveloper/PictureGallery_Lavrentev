//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PictureGallery_System_Lavrentev
{
    using System;
    using System.Collections.Generic;
    
    public partial class Post_Likes
    {
        public int Post_Like_Id { get; set; }
        public int Post_Id { get; set; }
        public string User_Login { get; set; }
    
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
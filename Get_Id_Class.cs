using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureGallery_System_Lavrentev
{
    public class Get_Id_Class
    {
        PictureGallery_LavrentevEntities10 db = new PictureGallery_LavrentevEntities10();

        public Get_Id_Class(PictureGallery_LavrentevEntities10 db)
        {
            this.db = db;
        }

        public int getLastId_Post()
        {
            int lastId = 0;

            foreach (Post p in db.Post)
            {
                lastId = p.Post_Id;
            }

            return lastId;
        }

        public int getLastId_Post_Likes()
        {
            int lastId = 0;

            foreach (Post_Likes p in db.Post_Likes)
            {
                lastId = p.Post_Like_Id;
            }

            return lastId;
        }

        public int getLastId_Comment_Likes()
        {
            int lastId = 0;

            foreach (Comment_Likes c in db.Comment_Likes)
            {
                lastId = c.Comment_Like_Id;
            }

            return lastId;
        }

        public int getLastId_Comment()
        {
            int lastId = 0;

            foreach (Comment c in db.Comment)
            {
                lastId = c.Comment_Id;
            }

            return lastId;
        }

        public int getId_PostType(string postTypeName)
        {
            foreach(Post_Type p in db.Post_Type)
            {
                if(p.Post_Type_Name == postTypeName)
                {
                    return p.Post_Type_Id;
                }
            }

            return -1;
        }
    }
}

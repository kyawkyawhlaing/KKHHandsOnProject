using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KKHHandsOnProject.Database.AppData;

[Table("TblBlog")]
public partial class BlogDataModel
{
    public int BlogId { get; set; }

    public string BlogTitle { get; set; } = null!;

    public string BlogAuthor { get; set; } = null!;

    public string BlogContent { get; set; } = null!;
    public string BlogImagePath { get; set; }

    public bool DeleteFlag { get; set; }
}

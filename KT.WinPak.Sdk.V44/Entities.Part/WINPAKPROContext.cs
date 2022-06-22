using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.Entities
{
    public partial class WINPAKPROContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            ////  Do：多对多配置联合主键    
            //modelBuilder.Entity<CardAccessLevels>().HasKey(l => new { l.CardId, l.AccessLevelId });

            ////配置Passage与PassageCategories的一对多关系
            //builder.HasOne(t => t.Passage).WithMany(p => p.PassageCategories).HasForeignKey(t => t.PassageId).OnDelete(DeleteBehavior.SetNull); 
             
        }

    }
}

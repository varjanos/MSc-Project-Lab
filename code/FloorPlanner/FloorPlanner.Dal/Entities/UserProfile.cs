﻿
using Core.Translation.Dal.Entities;

namespace FloorPlanner.Dal.Entities
{
    public class UserProfile
    {
        public const int DomainMaxLength = 256;
        public const int UserNameMaxLength = 256;

        public int Id { get; set; }

        public string Domain { get; set; }

        public string UserName { get; set; }
        public string LanguageName { get; set; }
        public Language Language { get; set; }
    }
}
using SavaDev.Base.Data.Enums;
using SavaDev.System.Data.Entities;
using SavaDev.System.Data.Entities.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.Base.Data
{
    internal class TestData
    {
        internal static IEnumerable<TestEntity> GetNewEntities()
        {
            return new List<TestEntity>
            {
                new TestEntity
                {
                    Id = 1,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 2,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 3,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 4,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 5,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 6,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 7,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 8,
                    IsDeleted = false,
                },
                new TestEntity
                {
                    Id = 9,
                    IsDeleted = true,
                },
                new TestEntity
                {
                    Id = 10,
                    IsDeleted = true,
                },
            };
        }

        internal static void ApplyFilters(ref IQueryable<TestEntity> list, TestFilterModel filter)
        {
            //list = list.ApplyIdFilter(filter).ApplyIsDeletedFilter(filter);
        }

        internal static IEnumerable<MailTemplate> GetMailTemplates()
        {
            return new List<MailTemplate>
            {
                new MailTemplate
                {
                    Id = 1,
                    Culture = "en",
                    Status = DocumentStatus.Outdated,
                    PermName= "doc1",
                    Title = "Doc 1",
                    Text = "This is a document 1",
                    IsDeleted = false
                },
                new MailTemplate
                {
                    Id = 2,
                    Culture = "en",
                    Status = DocumentStatus.Published,
                    PermName= "doc1",
                    Title = "Doc 1",
                    Text = "This is a document 1 first updated",
                    IsDeleted = false
                },
                new MailTemplate
                {
                    Id = 3,
                    Culture = "en",
                    Status = DocumentStatus.Draft,
                    PermName= "doc1",
                    Title = "Doc 1",
                    Text = "This is a document 1 second updated",
                    IsDeleted = false,
                    Created = new DateTime(2000, 1, 1, 0, 0, 0)
                },
                new MailTemplate
                {
                    Id = 4,
                    Culture = "ru",
                    Status = DocumentStatus.Published,
                    PermName= "doc1",
                    Title = "Док 1",
                    Text = "Это документ 1",
                    IsDeleted = false
                },
                new MailTemplate
                {
                    Id = 5,
                    Culture = "ru",
                    Status = DocumentStatus.Draft,
                    PermName= "doc1",
                    Title = "Док 1",
                    Text = "Это документ 1 впервые отредактированный",
                    IsDeleted = false
                },
                // Id 6-8: only one culture
                new MailTemplate
                {
                    Id = 6,
                    Culture = "en",
                    Status = DocumentStatus.Outdated,
                    PermName= "doc2",
                    Title = "Doc 2",
                    Text = "This is a document 2",
                    IsDeleted = false
                },
                new MailTemplate
                {
                    Id = 7,
                    Culture = "en",
                    Status = DocumentStatus.Published,
                    PermName= "doc2",
                    Title = "Doc 2",
                    Text = "This is a document 2 first updated",
                    IsDeleted = false
                },
                new MailTemplate
                {
                    Id = 8,
                    Culture = "en",
                    Status = DocumentStatus.Draft,
                    PermName= "doc2",
                    Title = "Doc 2",
                    Text = "This is a document 2 second updated",
                    IsDeleted = false
                },
                // Deleted
                new MailTemplate
                {
                    Id = 9,
                    Culture = "en",
                    Status = DocumentStatus.Draft,
                    PermName= "doc3",
                    Title = "Doc 3",
                    Text = "This is a document 3",
                    IsDeleted = true
                },
                // Published
                new MailTemplate
                {
                    Id = 10,
                    Culture = "en",
                    Status = DocumentStatus.Published,
                    PermName= "doc4",
                    Title = "Doc 4",
                    Text = "This is a document 4",
                    IsDeleted = false
                },
                // Outdated and published
                new MailTemplate
                {
                    Id = 11,
                    Culture = "en",
                    Status = DocumentStatus.Outdated,
                    PermName= "doc5",
                    Title = "Doc 5",
                    Text = "This is a document 5",
                    IsDeleted = false
                },
                new MailTemplate
                {
                    Id = 12,
                    Culture = "en",
                    Status = DocumentStatus.Published,
                    PermName= "doc5",
                    Title = "Doc 5",
                    Text = "This is a document 5 first updated",
                    IsDeleted = false
                },
                // Outdated
                new MailTemplate
                {
                    Id = 13,
                    Culture = "en",
                    Status = DocumentStatus.Outdated,
                    PermName= "doc6",
                    Title = "Doc 6",
                    Text = "This is a document 6",
                    IsDeleted = false
                },
                // Deleted
                new MailTemplate
                {
                    Id = 14,
                    Culture = "en",
                    Status = DocumentStatus.Draft,
                    PermName= "doc7",
                    Title = "Doc 7",
                    Text = "This is a document 7",
                    IsDeleted = true
                },
                new MailTemplate
                {
                    Id = 15,
                    Culture = "en",
                    Status = DocumentStatus.Outdated,
                    PermName= "doc7",
                    Title = "Doc 7",
                    Text = "This is a document 7",
                    IsDeleted = true
                },
                new MailTemplate
                {
                    Id = 16,
                    Culture = "en",
                    Status = DocumentStatus.Published,
                    PermName= "doc7",
                    Title = "Doc 7",
                    Text = "This is a document 7",
                    IsDeleted = true
                },
                new MailTemplate
                {
                    Id = 17,
                    Culture = "en",
                    Status = DocumentStatus.Draft,
                    PermName= "doc7",
                    Title = "Doc 7",
                    Text = "This is a document 7",
                    IsDeleted = false
                },
                new MailTemplate
                {
                    Id = 18,
                    Culture = "en",
                    Status = DocumentStatus.Published,
                    PermName= "doc8",
                    Title = "Doc 8",
                    Text = "This is a document 8",
                    IsDeleted = true
                },
                new MailTemplate
                {
                    Id = 19,
                    Culture = "en",
                    Status = DocumentStatus.Published,
                    PermName= "doc8",
                    Title = "Doc 8",
                    Text = "This is a document 8",
                    IsDeleted = false
                },
            };
        }
    }
}

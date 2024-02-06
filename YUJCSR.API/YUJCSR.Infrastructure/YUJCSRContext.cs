using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YUJCSR.Infrastructure.Model;

namespace YUJCSR.Infrastructure
{
    public class YUJCSRContext : DbContext
    {
        public YUJCSRContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<AdminUserModel>().ToTable("AdminUserMaster");
            model.Entity<CSOModel>().ToTable("CSOMaster");
            model.Entity<ProjectModel>().ToTable("ProjectMaster");
            model.Entity<ProjectBudgetModel>().ToTable("ProjectBudget");
            model.Entity<ProjectMilestoneModel>().ToTable("ProjectMilestones");
            model.Entity<ProjectCSOMappingModel>().ToTable("ProjectCSOMapping");
            model.Entity<UNSDGModel>().ToTable("UNSDGMaster");
            model.Entity<ProjectUNSDGMappingModel>().ToTable("ProjectUNSGDMapping");
            model.Entity<ImpactMasterModel>().ToTable("ImpactMaster");
            model.Entity<ProjectImpactModel>().ToTable("ProjectImpactMapping");
            model.Entity<CompanyModel>().ToTable("companymaster");
            model.Entity<CSODocModel>().ToTable("CSODocs");
            model.Entity<ProjectCompanyMappingModel>().ToTable("ProjectCompanyMapping");
            model.Entity<ProjectDocModel>().ToTable("ProjectDocs");
            //model.Entity<TenantLoginHistoryModel>().ToTable("TenantLoginHistory");
            //model.Entity<ExamMasterModel>().ToTable("ExamMaster");
            //model.Entity<ExamSetModel>().ToTable("ExamSet");
            //model.Entity<QuestionMasterModel>().ToTable("QuestionMaster");
            //model.Entity<QuestionChoice>().ToTable("QuestionChoice");
            //model.Entity<VWExamDetail>().ToView("VWExamDetails").HasNoKey();
            //model.Entity<ExamDetailResponseModel>().ToView("ExamDetailResponseModel").HasNoKey();
            //model.Entity<ExamSearchModel>().ToTable("ExamSearch");
            //model.Entity<ExamSummaryInfo>().ToView("ExamSummaryInfo").HasNoKey();

            //#region "Student"
            //model.Entity<StudentExamModel>().ToTable("StudentExam");
            //model.Entity<StudentExamDetailModel>().ToTable("StudentExamDetail");
            //model.Entity<StudentExamAuditModel>().ToTable("StudentExamAudit");
            //model.Entity<StudentForgotPasswordModel>().ToTable("StudentForgotPassword");
            //model.Entity<StudentModel>().ToTable("StudentMaster");


            //#endregion
        }
    }
}

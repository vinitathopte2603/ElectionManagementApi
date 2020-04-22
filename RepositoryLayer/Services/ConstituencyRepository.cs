using CommonLayer.Model;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interfaces;
using RepositoryLayer.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLayer.Services
{
    public class ConstituencyRepository : IConstituencyRepository
    {
        private readonly DBContext dBContext;
        public ConstituencyRepository(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }
      
        public Constituency AddConstituency(ConstituencyRequest constituencyRequest, int adminId)
        {
            try
            {
                var constituencyData = this.dBContext.constituencies.FirstOrDefault(linq => linq.ConstituencyName == constituencyRequest.ConstituencyName);
                if (constituencyData == null)
                {
                    Constituency constituency = new Constituency()
                    {
                        ConstituencyName = constituencyRequest.ConstituencyName,
                        City= constituencyRequest.City,
                        State = constituencyRequest.State,
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        IsDeleted = false,
                        AdminId = adminId
                    };
                    this.dBContext.constituencies.Add(constituency);
                    this. dBContext.SaveChanges();
                    return constituency;
                }
                return null;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteConstituency(int constituencyId, int adminId)
        {
            try
            {
                var constituency = this.dBContext.constituencies.FirstOrDefault(linq => linq.ConstituencyId == constituencyId && linq.IsDeleted == false);
                if (constituency != null)
                {
                    constituency.AdminId = adminId;
                    constituency.Modified = DateTime.Now;
                    constituency.IsDeleted = true;
                    var constituencyData = this.dBContext.constituencies.Attach(constituency);
                    constituencyData.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    this.dBContext.SaveChanges();
                    if (constituencyData.State != 0)
                    {
                        var data = new AdminConstituency
                        {
                            ConstituencyId = constituencyId,
                            AdminId = adminId
                        };
                        this.dBContext.adminConstituencies.Add(data);
                        this.dBContext.SaveChanges();

                    }
                    return true;
                }
                return false;
            }
          catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Constituency> GetAllConstituencies(string state)
        {
            List<Constituency> constituencies = new List<Constituency>();
            if (string.IsNullOrWhiteSpace(state))
            {
                constituencies = dBContext.constituencies.Where(linq => linq.IsDeleted == false).Select
               (linq => new Constituency
               {
                   ConstituencyId = linq.ConstituencyId,
                   ConstituencyName = linq.ConstituencyName,
                   State = linq.State,
                   City = linq.City,
                   AdminId = linq.AdminId,
                   Created = linq.Created,
                   IsDeleted = linq.IsDeleted,
                   Modified = linq.Modified,


               }).ToList();
            }
            else
            {
                constituencies = dBContext.constituencies.Where(linq => linq.IsDeleted == false && linq.State == state).Select
               (linq => new Constituency
               {
                   ConstituencyId = linq.ConstituencyId,
                   ConstituencyName = linq.ConstituencyName,
                   State = linq.State,
                   City = linq.City,
                   AdminId = linq.AdminId,
                   Created = linq.Created,
                   IsDeleted = linq.IsDeleted,
                   Modified = linq.Modified,


               }).ToList();
            }
            
            foreach (var admindata in constituencies)
            {
                admindata.AdminResponses = Adminresponse(admindata.ConstituencyId);

            }
            if (constituencies.Count != 0)
            {
                return constituencies;
            }
            return null;
        }
        public List<NameResponse> GetAllStates()
        {
            var data = this.dBContext.constituencies.Select(linq => new NameResponse() { Name = linq.State }).ToList();
            List<NameResponse> states = new List<NameResponse>();
            foreach(var name in data)
            {
                if (states.Any(linq => linq.Name == name.Name) == false)
                {
                    states.Add(name);
                }
                
            }
            if (states.Count != 0)
            {
                return states;
            }
            return null;
        }

        public Constituency UpdateConstituency(int constituencyId, ConstituencyDataRequest constituencyDataRequest,int adminId)
        {


            Constituency constituency = this.dBContext.constituencies.FirstOrDefault(linq => linq.ConstituencyId == constituencyId && linq.IsDeleted == false);
            if (constituency != null)
            {
                constituency.AdminId = constituency.AdminId;
                constituency.City = constituencyDataRequest.City;
                constituency.ConstituencyName = constituencyDataRequest.ConstituencyName;
                constituency.State = constituencyDataRequest.State;
                constituency.Modified = DateTime.Now;
                var constituencyData = this.dBContext.constituencies.Attach(constituency);
                constituencyData.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this.dBContext.SaveChanges();
                if (constituencyData.State != 0)
                {
                    var data = new AdminConstituency
                    {
                        ConstituencyId = constituencyId,
                        AdminId = adminId
                    };
                    this.dBContext.adminConstituencies.Add(data);
                    this.dBContext.SaveChanges();
                    constituency.AdminResponses = Adminresponse(constituencyId);

                }
                
                return constituency;
            }

          
          
            return null;
        }
        private List<AdminResponse> Adminresponse (int constituencyId)
        {
            List<AdminResponse> adminResponse = dBContext.adminConstituencies.Where(linq => linq.ConstituencyId == constituencyId).Join
                    (dBContext.admins,
                    admn => admn.AdminId,
                    consi => consi.AdminId,
                    (consi, adm) => new AdminResponse
                    {
                        AdminId = adm.AdminId,
                        AdminFirstName = adm.AdmFirstName,
                        AdminLastName = adm.AdmLastName,
                        AdminContactNumber = adm.AdmContactNumber,
                        AdminEmail = adm.AdmEmail,
                    }
                    ).ToList();
            return adminResponse;
        }
    }
}

using DataLayer.Entities;
using DTO.ReqDTO;
using DTO.ResDTO;
using Helper.CommonHelpers;
using Helper.CommonModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class UserBLL
    {
        public readonly CommonHelper _commonHelper;
        public readonly CommonRepo _commonRepo;
        public readonly DBContext _dbContext;
        public UserBLL(CommonHelper commonHelper, CommonRepo commonRepo, DBContext dbContext)
        {
            _commonHelper = commonHelper;
            _commonRepo = commonRepo;
            _dbContext = dbContext;
        }
        public async Task<CommonResponse> GetAllUserDetailList()
        {
            CommonResponse response = new CommonResponse();
            GetAllUserDetailListResDTO resDTO = new GetAllUserDetailListResDTO();
            List<GetAllUserDetailList> getAllUsers = new List<GetAllUserDetailList>(); 
            try
            {
                var userList = await _commonRepo.UserMstList().ToListAsync();
                if (userList.Count > 0)
                {
                    foreach (var item in userList)
                    {
                        GetAllUserDetailList getUser = new GetAllUserDetailList();
                        getUser.Id = item.Id;
                        getUser.FirstName = item.FirstName;
                        getUser.LastName = item.LastName;
                        getUser.Email = item.Email;
                        getUser.Address = item.Address;
                        getUser.ContactNo = item.ContactNo;
                        getUser.DesignationId = item.DesignationId;
                        getUser.DepartmentId = item.DepartmentId;
                        getUser.UserStatusId = item.UserStatusId;
                        getUser.UserType = item.UserType;

                        getAllUsers.Add(getUser);
                    }

                    resDTO.GetAllUserDetailLists = getAllUsers;
                    resDTO.TotalCounts = getAllUsers.Count;

                    response.Status = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = CommonConstant.DataFoundSuccessfully;
                    response.Data = resDTO;
                }
                else
                {
                    response.Message = CommonConstant.DataNotFound;
                    response.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch { throw; }
            return response;
        }
        public async Task<CommonResponse> AddEditUser(AddEditUserReqDTO request)
        {
            CommonResponse response = new CommonResponse();
            try
            {
                UserMst userMst = new UserMst();
                DateTime currentDateTime = _commonHelper.GetCurrentDateTime();
                int loggedInUserId = _commonHelper.GetLoggedInUserIdAsync();
                if (request.Id > 0 && request.Id != 0)
                {
                    //Edit Mode
                    var userExist = await _commonRepo.UserMstList().FirstOrDefaultAsync(x => x.Id == request.Id);
                    if (userExist != null)
                    {
                        bool duplicateEmail = await _commonRepo.UserMstList().AnyAsync(x => x.Email == request.Email);
                        if (!duplicateEmail)
                        {
                            userExist.FirstName = request.FirstName;
                            userExist.LastName = request.LastName;
                            userExist.Email = request.Email;
                            userExist.ContactNo = request.ContactNo;
                            userExist.Address = request.Address;
                            userExist.DesignationId = request.DesignationId;
                            userExist.DepartmentId = request.DepartmentId;
                            userExist.UserStatusId = request.UserStatusId;
                            userExist.UserType = request.UserType;
                            userExist.UpdatedBy = loggedInUserId;
                            userExist.UpdatedDate = currentDateTime;

                            _dbContext.Entry(userExist).State = EntityState.Modified;
                            await _dbContext.SaveChangesAsync();

                            response.Data = userExist;
                            response.Message = "User details updated successfully!";
                            response.Status = true;
                            response.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            response.Message = "This email is already in use!";
                            response.StatusCode = HttpStatusCode.BadRequest;
                        }
                    }
                    else
                    {
                        response.Message = "Invalid User!";
                        response.StatusCode = HttpStatusCode.NotFound;
                    }
                }
                else
                {
                    //Add Mode

                    var duplicateCheck = await _commonRepo.UserMstList().FirstOrDefaultAsync(x => x.Email == request.Email);
                    if (duplicateCheck == null)
                    {
                        userMst.FirstName = request.FirstName;
                        userMst.LastName = request.LastName;
                        userMst.Email = request.Email;
                        userMst.ContactNo = request.ContactNo;
                        userMst.Address = request.Address;
                        userMst.Password = request.Password;
                        userMst.UserStatusId = request.UserStatusId;
                        userMst.UserType = request.UserType;
                        userMst.DesignationId = request.DesignationId;
                        userMst.DepartmentId = request.DepartmentId;
                        userMst.IsActive = true;
                        userMst.IsDelete = false;
                        userMst.CreatedBy = loggedInUserId;
                        userMst.UpdatedBy = loggedInUserId;
                        userMst.CreatedDate = currentDateTime;
                        userMst.UpdatedDate = currentDateTime;

                        await _dbContext.AddAsync(userMst);
                        await _dbContext.SaveChangesAsync();

                        response.Status = true;
                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = CommonConstant.DataSavedSuccessfully;
                        response.Data = userMst;
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.BadRequest;
                        response.Message = "This email is already registered!";
                    }
                }
            }
            catch { throw; }
            return response;
        }
    }
}

using DataLayer;
using DTO;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLayer
{
    public class UserBLL
    {
        private readonly HIMSUserDBContext _context;

        public UserBLL(HIMSUserDBContext context)
        {
            this._context = context;
        }

        public async Task<ResponseDTO> GetAllUsersAsync()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                dynamic data = await (from users in _context.UserMsts
                                      join role in _context.RoleMsts on users.RoleId equals role.RoleId into roles
                                      from roledata in roles.DefaultIfEmpty()
                                      select new UserMstDTO
                                      {
                                          UserId = users.UserId,
                                          RoleId = (int?)roledata.RoleId ?? users.RoleId,
                                          RoleName = roledata.RoleName ?? "",
                                          UserName = users.UserName,
                                          Password = users.Password,
                                          IsActive = users.IsActive,
                                          IsDeleted = users.IsDeleted,
                                          CreateBy = users.CreateBy,
                                          UpdatedBy = users.UpdatedBy,
                                          CreateAt = users.CreateAt,
                                          UpdatedAt = users.UpdatedAt
                                      }).ToListAsync();

                //dynamic data = await _context.UserMsts.Where(x=> x.IsActive && !x.IsDeleted).ToListAsync();
                responseDTO.Data = data; ;
                responseDTO.Message = "Success";
                responseDTO.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAllDoctorNameAsync()
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<DoctorDetail> doctorDetail = await _context.DoctorDetails.Where(x => x.IsActive && !x.IsDelete).ToListAsync() ?? new List<DoctorDetail>();
                List<DoctorNameResponseDTO> doctorNameResponseDTOs = new List<DoctorNameResponseDTO>();
                DoctorNameResponseDTO doctorNameResponseDTO;

                foreach (var item in doctorDetail)
                {
                    doctorNameResponseDTO = new DoctorNameResponseDTO();

                    doctorNameResponseDTO.DoctorId = item.DoctorId;
                    doctorNameResponseDTO.FullName = item.FirstName + " " + item.LastName;

                    doctorNameResponseDTOs.Add(doctorNameResponseDTO);
                }

                responseDTO.Data = doctorNameResponseDTOs;

                if (doctorNameResponseDTOs.Count > 0)
                {
                    responseDTO.Message = "Doctor Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Message = "Doctor Data No Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAllPatientFirstNameAsync(string FirstName)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<PatientDetail> patientDetail = await _context.PatientDetails.Where(x => x.FirstName.Substring(0, FirstName.Length) == FirstName && x.IsActive && !x.IsDelete).GroupBy(p => p.FirstName).Select(g => g.First()).ToListAsync() ?? new List<PatientDetail>();

                if (patientDetail != null && patientDetail.Count > 0)
                {
                    List<PatientNameResponseDTO> patientNameResponseDTOs = new List<PatientNameResponseDTO>();
                    PatientNameResponseDTO patientNameResponseDTO;

                    foreach (var item in patientDetail)
                    {
                        patientNameResponseDTO = new PatientNameResponseDTO();

                        patientNameResponseDTO.PatientId = item.PatientId;
                        patientNameResponseDTO.FirstName = item.FirstName;

                        patientNameResponseDTOs.Add(patientNameResponseDTO);
                    }
                    responseDTO.Data = patientNameResponseDTOs;
                    responseDTO.Message = "All Patient's First Name Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "No Patient Found By This First Name!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAllPatientLastNameAsync(string FirstName)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                List<PatientDetail> patientDetail = await _context.PatientDetails.Where(x => x.IsActive && !x.IsDelete && x.FirstName == FirstName).ToListAsync() ?? new List<PatientDetail>();

                if (patientDetail != null && patientDetail.Count > 0)
                {
                    List<PatientNameResponseDTO> patientNameResponseDTOs = new List<PatientNameResponseDTO>();
                    PatientNameResponseDTO patientNameResponseDTO;

                    foreach (var item in patientDetail)
                    {
                        patientNameResponseDTO = new PatientNameResponseDTO();

                        patientNameResponseDTO.PatientId = item.PatientId;
                        patientNameResponseDTO.FirstName = item.FirstName;
                        patientNameResponseDTO.LastName = item.LastName;

                        patientNameResponseDTOs.Add(patientNameResponseDTO);
                    }

                    responseDTO.Data = patientNameResponseDTOs;
                    responseDTO.Message = "Patient's Last Name Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "No Patient Found By This First Name!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }

            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetPatientDetailsAsync(int PatientId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                PatientResponseDTO patientResponseDTO = new PatientResponseDTO();
                List<PatientResponseDTO> listPatientResponseDTO = new List<PatientResponseDTO>();
                if (PatientId != 0)
                {
                    PatientDetail patientDetail = await _context.PatientDetails.Where(x => x.PatientId == PatientId).SingleOrDefaultAsync();

                    if (patientDetail != null)
                    {
                        patientResponseDTO.PatientId = patientDetail.PatientId;
                        patientResponseDTO.PatientCode = patientDetail.PatientCode;
                        patientResponseDTO.UserId = patientDetail.UserId;
                        patientResponseDTO.FirstName = patientDetail.FirstName;
                        patientResponseDTO.LastName = patientDetail.LastName;
                        patientResponseDTO.Gender = patientDetail.Gender;
                        patientResponseDTO.Dob = patientDetail.Dob;
                        patientResponseDTO.Email = patientDetail.Email;
                        patientResponseDTO.MobileNo = patientDetail.MobileNo;
                        patientResponseDTO.AlternateMobileNo = patientDetail.AlternateMobileNo;
                        patientResponseDTO.Address = patientDetail.Address;
                        patientResponseDTO.StreetLandMark = patientDetail.StreetLandMark;
                        patientResponseDTO.State = patientDetail.State;
                        patientResponseDTO.City = patientDetail.City;
                        patientResponseDTO.ImagePath = patientDetail.Pincode;
                        patientResponseDTO.Image = patientDetail.Image;
                        patientResponseDTO.ImagePath = patientDetail.ImagePath;

                        responseDTO.Data = patientDetail;
                        responseDTO.Message = "Patient Data Found!";
                        responseDTO.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        responseDTO.Data = patientDetail;
                        responseDTO.Message = "Patient Data Not Found!";
                        responseDTO.StatusCode = HttpStatusCode.OK;
                    }

                }
                else
                {
                    List<PatientDetail> patientDetail = await _context.PatientDetails.Where(x => x.IsActive && !x.IsDelete).ToListAsync() ?? new List<PatientDetail>();

                    foreach (var item in patientDetail)
                    {
                        patientResponseDTO = new PatientResponseDTO();

                        patientResponseDTO.PatientId = item.PatientId;
                        patientResponseDTO.PatientCode = item.PatientCode;
                        patientResponseDTO.UserId = item.UserId;
                        patientResponseDTO.FirstName = item.FirstName;
                        patientResponseDTO.LastName = item.LastName;
                        patientResponseDTO.Gender = item.Gender;
                        patientResponseDTO.Dob = item.Dob;
                        patientResponseDTO.Email = item.Email;
                        patientResponseDTO.MobileNo = item.MobileNo;
                        patientResponseDTO.AlternateMobileNo = item.AlternateMobileNo;
                        patientResponseDTO.Address = item.Address;
                        patientResponseDTO.StreetLandMark = item.StreetLandMark;
                        patientResponseDTO.State = item.State;
                        patientResponseDTO.City = item.City;
                        patientResponseDTO.ImagePath = item.Pincode;
                        patientResponseDTO.Image = item.Image;
                        patientResponseDTO.ImagePath = item.ImagePath;

                        listPatientResponseDTO.Add(patientResponseDTO);
                    }

                    responseDTO.Data = listPatientResponseDTO;
                    responseDTO.Message = "Patient Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetPatientAndDoctorDetailsAsync(int AilmentId, int DoctorId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                PatientResponseDTO patientResponseDTO = await (from ailmentMst in _context.AilmentMsts
                                  where ailmentMst.AilmentId == AilmentId
                                  join patientDetail in _context.PatientDetails on ailmentMst.PatientId equals patientDetail.PatientId into patient
                                  from patientData in patient.DefaultIfEmpty()
                                  select new PatientResponseDTO
                                  {
                                      PatientId = ailmentMst.PatientId,
                                      PatientCode = patientData.PatientCode,
                                      UserId = patientData.UserId,
                                      FirstName = patientData.FirstName,
                                      LastName = patientData.LastName,
                                      Gender = patientData.Gender,
                                      Dob = patientData.Dob,
                                      Email = patientData.Email,
                                      MobileNo = patientData.MobileNo,
                                      AlternateMobileNo = patientData.AlternateMobileNo,
                                      Address = patientData.Address,
                                      StreetLandMark = patientData.StreetLandMark,
                                      State = patientData.State,
                                      City = patientData.City,
                                      Pincode = patientData.Pincode,
                                      Image = patientData.Image,
                                      ImagePath = patientData.ImagePath
                                  }).SingleOrDefaultAsync() ?? new PatientResponseDTO();

                DoctorNameResponseDTO doctorNameResponseDTO = await (from doctor in _context.DoctorDetails
                                                                     where doctor.DoctorId == DoctorId
                                                                     select new DoctorNameResponseDTO
                                                                     {
                                                                         DoctorId = doctor.DoctorId,
                                                                         FullName = doctor.FirstName + " " + doctor.LastName,
                                                                     }).SingleOrDefaultAsync() ?? new  DoctorNameResponseDTO();

                PatientAndDoctorResponseDTO patientAndDoctorResponseDTO = new PatientAndDoctorResponseDTO();
                patientAndDoctorResponseDTO.PatientResponseDTO = patientResponseDTO;
                patientAndDoctorResponseDTO.DoctorNameResponseDTO = doctorNameResponseDTO;

                responseDTO.Data = patientAndDoctorResponseDTO;
                responseDTO.Message = "Patient And Doctor Details Found!";
                responseDTO.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetPatientAilmentDetailsAsync(int AilmentId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                AilmentResponseDTO ailmentResponseDTO = new AilmentResponseDTO();

                var PatientDetails = await (from ailmentMst in _context.AilmentMsts
                                            join patientDetail in _context.PatientDetails on ailmentMst.PatientId equals patientDetail.PatientId into patient
                                            from patientData in patient.DefaultIfEmpty()
                                            where ailmentMst.AilmentId == AilmentId
                                            select new PatientResponseDTO
                                            {
                                                PatientId = ailmentMst.PatientId,
                                                PatientCode = patientData.PatientCode,
                                                UserId = patientData.UserId,
                                                FirstName = patientData.FirstName,
                                                LastName = patientData.LastName,
                                                Gender = patientData.Gender,
                                                Dob = patientData.Dob,
                                                Email = patientData.Email,
                                                MobileNo = patientData.MobileNo,
                                                AlternateMobileNo = patientData.AlternateMobileNo,
                                                Address = patientData.Address,
                                                StreetLandMark = patientData.StreetLandMark,
                                                State = patientData.State,
                                                City = patientData.City,
                                                Pincode = patientData.Pincode,
                                                Image = patientData.Image,
                                                ImagePath = patientData.ImagePath,
                                            }).SingleOrDefaultAsync();

                var PatientSymptomDetails = await (from patientSymptomDetails in _context.PatientSymptomDetails
                                                   join symptomMst in _context.SymptomMsts on patientSymptomDetails.SymptomId equals symptomMst.SymptomId into symptom
                                                   from symptomData in symptom.DefaultIfEmpty()
                                                   where patientSymptomDetails.AilmentId == AilmentId
                                                   select new PatientSymptomResponseDTO
                                                   {
                                                       SymptomId = patientSymptomDetails.SymptomId,
                                                       SymptomName = symptomData.SymptomName,
                                                       IsActive = symptomData.IsActive,
                                                       IsDelete = symptomData.IsDelete,
                                                       CreatedBy = symptomData.CreatedBy,
                                                       UpdatedBy = symptomData.UpdateBy,
                                                       CreatedAt = symptomData.CreatedAt,
                                                       UpdatedAt = symptomData.UpdatedAt,
                                                   }).ToListAsync();

                var AilmentImageDetails = await _context.AilmentImageDetails.Where(x => x.AilmentId == AilmentId).ToListAsync();

                AilmentImageResponseDTO ailmentImageResponseDTO;
                List<AilmentImageResponseDTO> ailmentImageResponseDTOs = new List<AilmentImageResponseDTO>();

                foreach (var item in AilmentImageDetails)
                {
                    ailmentImageResponseDTO = new AilmentImageResponseDTO();
                    ailmentImageResponseDTO.AilmentImageId = item.AilmentImageId;
                    ailmentImageResponseDTO.Image = item.Image;

                    ailmentImageResponseDTOs.Add(ailmentImageResponseDTO);
                }

                /*ailmentResponseDTO.patientResponseDTO = PatientDetails;*/

                ailmentResponseDTO.patientSymptomResponseDTOs = PatientSymptomDetails;
                ailmentResponseDTO.ailmentImageResponseDTOs = ailmentImageResponseDTOs;
                if (PatientDetails != null)
                {
                    ailmentResponseDTO.patientSymptomResponseDTOs = PatientSymptomDetails;
                    ailmentResponseDTO.PatientId = PatientDetails.PatientId;
                    ailmentResponseDTO.PatientCode = PatientDetails.PatientCode;
                    ailmentResponseDTO.UserId = PatientDetails.UserId;
                    ailmentResponseDTO.FirstName = PatientDetails.FirstName;
                    ailmentResponseDTO.LastName = PatientDetails.LastName;
                    ailmentResponseDTO.Gender = PatientDetails.Gender;
                    ailmentResponseDTO.Dob = PatientDetails.Dob;
                    ailmentResponseDTO.Email = PatientDetails.Email;
                    ailmentResponseDTO.MobileNo = PatientDetails.MobileNo;
                    ailmentResponseDTO.AlternateMobileNo = PatientDetails.AlternateMobileNo;
                    ailmentResponseDTO.Address = PatientDetails.Address;
                    ailmentResponseDTO.StreetLandMark = PatientDetails.StreetLandMark;
                    ailmentResponseDTO.State = PatientDetails.State;
                    ailmentResponseDTO.City = PatientDetails.City;
                    ailmentResponseDTO.Pincode = PatientDetails.Pincode;
                    ailmentResponseDTO.Image = PatientDetails.Image;
                    ailmentResponseDTO.ImagePath = PatientDetails.ImagePath;

                    responseDTO.Data = ailmentResponseDTO;
                    responseDTO.Message = "Patient's Ailment Details Found!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = ailmentResponseDTO;
                    responseDTO.Message = "Patient's Ailment Details Not Found!";
                    responseDTO.StatusCode = HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAllSymptomsAsync(int SymptomId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                SymptomResponseDTO symptomResponseDTOs = new SymptomResponseDTO();
                List<SymptomResponseDTO> listSymptomResponseDTOs = new List<SymptomResponseDTO>();
                if (SymptomId != 0)
                {
                    SymptomMst symptomMst = await _context.SymptomMsts.Where(x => x.SymptomId == SymptomId).SingleOrDefaultAsync();
                    if (symptomMst != null)
                    {
                        symptomResponseDTOs.SymptomId = symptomMst.SymptomId;
                        symptomResponseDTOs.SymptomName = symptomMst.SymptomName;

                        responseDTO.Data = symptomResponseDTOs;
                        responseDTO.Message = "Symptom Data Found!";
                        responseDTO.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        responseDTO.Data = null;
                        responseDTO.Message = "Symptom Data Not Found!";
                        responseDTO.StatusCode = HttpStatusCode.NotFound;
                    }
                }
                else
                {
                    List<SymptomMst> symptomMst = await _context.SymptomMsts.Where(x => x.IsActive && !x.IsDelete).ToListAsync();

                    foreach (var item in symptomMst)
                    {
                        symptomResponseDTOs = new SymptomResponseDTO();

                        symptomResponseDTOs.SymptomId = item.SymptomId;
                        symptomResponseDTOs.SymptomName = item.SymptomName;

                        listSymptomResponseDTOs.Add(symptomResponseDTOs);
                    }

                    responseDTO.Data = listSymptomResponseDTOs;
                    responseDTO.Message = "Symptom Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        /*public async Task<ResponseDTO> AddAilmentImageAsync(AilmentImageRequestDTO ailmentImageRequestDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                if (ailmentImageRequestDTO != null)
                {
                    AilmentImageDetail ailmentImageDetail = new AilmentImageDetail();
                    ailmentImageDetail.AilmentId = ailmentImageRequestDTO.AilmentId;
                    ailmentImageDetail.Image = ailmentImageRequestDTO.Image;
                    ailmentImageDetail.IsDelete = false;
                    ailmentImageDetail.CreatedBy = ailmentImageRequestDTO.CreatedBy;
                    ailmentImageDetail.UpdateBy = ailmentImageRequestDTO.CreatedBy;
                    ailmentImageDetail.CreatedAt = DateTime.Now;
                    ailmentImageDetail.UpdatedAt = DateTime.Now;

                    _context.AilmentImageDetails.Add(ailmentImageDetail);
                    await _context.SaveChangesAsync();

                    responseDTO.Data = ailmentImageRequestDTO;
                    responseDTO.Message = "Data Saved Successfully!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = ailmentImageRequestDTO;
                    responseDTO.Message = "No Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex;
                responseDTO.Message = "Exception";
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }*/

        public async Task<ResponseDTO> AddUpdatePatientAsync(PatientSymptomRequestDTO patientSymptomRequestDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                PatientRequestDTO patientRequestDTO = patientSymptomRequestDTO.PatientRequestDTO;
                if (patientRequestDTO != null)
                {
                    if (patientRequestDTO.PatientId != 0)
                    {
                        PatientDetail patientDetail = await _context.PatientDetails.Where(x => x.PatientId == patientRequestDTO.PatientId).SingleOrDefaultAsync();
                        if (patientDetail != null)
                        {
                            patientDetail.FirstName = patientRequestDTO.FirstName;
                            patientDetail.LastName = patientRequestDTO.LastName;
                            patientDetail.Gender = patientRequestDTO.Gender;
                            patientDetail.Dob = patientRequestDTO.Dob;
                            patientDetail.Email = patientRequestDTO.Email;
                            patientDetail.MobileNo = patientRequestDTO.MobileNo;
                            patientDetail.AlternateMobileNo = patientRequestDTO.AlternateMobileNo;
                            patientDetail.Address = patientRequestDTO.Address;
                            patientDetail.StreetLandMark = patientRequestDTO.StreetLandMark;
                            patientDetail.State = patientRequestDTO.State;
                            patientDetail.City = patientRequestDTO.City;
                            patientDetail.Pincode = patientRequestDTO.Pincode;
                            patientDetail.Image = patientRequestDTO.Image;
                            patientDetail.ImagePath = patientRequestDTO.ImagePath;
                            patientDetail.IsActive = patientRequestDTO.IsActive;
                            patientDetail.UpdateBy = patientRequestDTO.CreatedBy;
                            patientDetail.UpdatedAt = DateTime.Now;

                            _context.Entry(patientDetail).State = EntityState.Modified;
                            await _context.SaveChangesAsync();

                            responseDTO.Data = patientRequestDTO;
                            responseDTO.Message = "Data Updated Successfully!";
                            responseDTO.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            responseDTO.Data = patientRequestDTO;
                            responseDTO.Message = "No Data Found!";
                            responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
                        }
                    }
                    else
                    {
                        UserMst userMst = new UserMst();
                        userMst.RoleId = 2; // Patient RoleId
                        userMst.UserName = patientRequestDTO.Email;
                        userMst.Password = patientRequestDTO.FirstName.Substring(0, 3).ToUpper() + patientRequestDTO.Dob?.ToString("ddmm");
                        userMst.IsActive = patientRequestDTO.IsActive;
                        userMst.IsDeleted = false;
                        userMst.CreateBy = patientRequestDTO.CreatedBy;
                        userMst.UpdatedBy = patientRequestDTO.CreatedBy;
                        userMst.CreateAt = DateTime.Now;
                        userMst.UpdatedAt = DateTime.Now;

                        await _context.UserMsts.AddAsync(userMst);
                        await _context.SaveChangesAsync();

                        PatientDetail patientDetail = new PatientDetail();
                        patientDetail.PatientCode = GeneratePatientUniqueCode(patientRequestDTO.FirstName, patientRequestDTO.LastName);
                        patientDetail.UserId = userMst.UserId;
                        patientDetail.FirstName = patientRequestDTO.FirstName;
                        patientDetail.LastName = patientRequestDTO.LastName;
                        patientDetail.Gender = patientRequestDTO.Gender;
                        patientDetail.Dob = patientRequestDTO.Dob;
                        patientDetail.Email = patientRequestDTO.Email;
                        patientDetail.MobileNo = patientRequestDTO.MobileNo;
                        patientDetail.AlternateMobileNo = patientRequestDTO.AlternateMobileNo;
                        patientDetail.Address = patientRequestDTO.Address;
                        patientDetail.StreetLandMark = patientRequestDTO.StreetLandMark;
                        patientDetail.State = patientRequestDTO.State;
                        patientDetail.City = patientRequestDTO.City;
                        patientDetail.Pincode = patientRequestDTO.Pincode;
                        patientDetail.Image = patientRequestDTO.Image;
                        patientDetail.ImagePath = patientRequestDTO.ImagePath;
                        patientDetail.IsActive = patientRequestDTO.IsActive;
                        patientDetail.IsDelete = false;
                        patientDetail.CreatedBy = patientRequestDTO.CreatedBy;
                        patientDetail.UpdateBy = patientRequestDTO.CreatedBy;
                        patientDetail.CreatedAt = DateTime.Now;
                        patientDetail.UpdatedAt = DateTime.Now;

                        await _context.PatientDetails.AddAsync(patientDetail);
                        await _context.SaveChangesAsync();

                        patientRequestDTO.PatientId = patientDetail.PatientId;

                        responseDTO.Data = patientRequestDTO;
                        responseDTO.Message = "Data Added Successfully!";
                        responseDTO.StatusCode = HttpStatusCode.OK;
                    }

                    AilmentMst ailmentMst = new AilmentMst();
                    if (patientSymptomRequestDTO.AilmentId == 0)
                    {
                        // Add Ailment Data
                        ailmentMst.PatientId = patientRequestDTO.PatientId;
                        ailmentMst.IsActive = true;
                        ailmentMst.IsDelete = false;
                        ailmentMst.CreatedBy = patientRequestDTO.CreatedBy;
                        ailmentMst.UpdateBy = patientRequestDTO.CreatedBy;
                        ailmentMst.CreatedAt = DateTime.Now;
                        ailmentMst.UpdatedAt = DateTime.Now;

                        await _context.AilmentMsts.AddAsync(ailmentMst);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        ailmentMst.AilmentId = patientSymptomRequestDTO.AilmentId;
                    }

                    // Add Updated Symptom
                    List<SymptomRequestDTO> symptomRequestDTO = patientSymptomRequestDTO.SymptomRequestDTOs;
                    if (symptomRequestDTO != null && symptomRequestDTO.Count > 0)
                    {
                        var availableSymptoms = await _context.PatientSymptomDetails.Where(x => x.AilmentId == patientSymptomRequestDTO.AilmentId).ToListAsync();
                        List<PatientSymptomDetail> removableSymptoms = availableSymptoms.Where(x => !symptomRequestDTO.Any(y => y.SymptomId == x.SymptomId)).ToList() ?? new List<PatientSymptomDetail>();
                        foreach (var patientSymptom in removableSymptoms)
                        {
                            _context.PatientSymptomDetails.Remove(patientSymptom);
                            await _context.SaveChangesAsync();
                        }

                        List<SymptomRequestDTO> toAddSymptoms = symptomRequestDTO.Where(x => !availableSymptoms.Any(y => y.SymptomId == x.SymptomId)).ToList() ?? new List<SymptomRequestDTO>();
                        foreach (var symptom in toAddSymptoms)
                        {
                            SymptomMst symptomMst = await _context.SymptomMsts.Where(x => x.SymptomName.ToLower() == symptom.SymptomName.ToLower()).FirstOrDefaultAsync();

                            //SymptomMst symptomMst = await _context.SymptomMsts.Where(x => x.SymptomId == symptom.SymptomId).SingleOrDefaultAsync();

                            if (symptomMst != null)
                            {
                                symptomMst.SymptomName = symptom.SymptomName;
                                symptomMst.IsActive = true;
                                symptomMst.UpdateBy = patientRequestDTO.CreatedBy;
                                symptomMst.UpdatedAt = DateTime.Now;

                                _context.Entry(symptomMst).State = EntityState.Modified;
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                symptomMst = new SymptomMst();
                                symptomMst.SymptomName = symptom.SymptomName;
                                symptomMst.IsActive = true;
                                symptomMst.CreatedBy = patientRequestDTO.CreatedBy;
                                symptomMst.UpdateBy = patientRequestDTO.CreatedBy;
                                symptomMst.CreatedAt = DateTime.Now;
                                symptomMst.UpdatedAt = DateTime.Now;

                                await _context.SymptomMsts.AddAsync(symptomMst);
                                await _context.SaveChangesAsync();

                                symptom.SymptomId = symptomMst.SymptomId;
                            }

                            PatientSymptomDetail patientSymptomDetail = new PatientSymptomDetail();
                            patientSymptomDetail.SymptomId = symptom.SymptomId;
                            patientSymptomDetail.AilmentId = ailmentMst.AilmentId;
                            patientSymptomDetail.IsActive = true;
                            patientSymptomDetail.IsDelete = false;
                            patientSymptomDetail.CreatedBy = patientRequestDTO.CreatedBy;
                            patientSymptomDetail.UpdateBy = patientRequestDTO.CreatedBy;
                            patientSymptomDetail.CreatedAt = DateTime.Now;
                            patientSymptomDetail.UpdatedAt = DateTime.Now;

                            await _context.PatientSymptomDetails.AddAsync(patientSymptomDetail);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Add Update Ailment Image
                    List<AilmentImageRequestDTO> ailmentImageRequestDTOs = patientSymptomRequestDTO.AilmentImageRequestDTOs;
                    if (ailmentImageRequestDTOs != null && ailmentImageRequestDTOs.Count > 0)
                    {
                        foreach (var ailmentImage in ailmentImageRequestDTOs)
                        {
                            AilmentImageDetail ailmentImageDetail = await _context.AilmentImageDetails.Where(x => x.AilmentImageId == ailmentImage.AilmentImageId).SingleOrDefaultAsync();

                            if (ailmentImage.AilmentImageId != 0 && ailmentImageDetail != null)
                            {
                                ailmentImageDetail.Image = ailmentImage.Image;
                                ailmentImageDetail.UpdateBy = patientRequestDTO.CreatedBy;
                                ailmentImageDetail.UpdatedAt = DateTime.Now;

                                _context.Entry(ailmentImageDetail).State = EntityState.Modified;
                                await _context.SaveChangesAsync();
                            }
                            else
                            {
                                ailmentImageDetail = new AilmentImageDetail();
                                ailmentImageDetail.AilmentId = ailmentMst.AilmentId;
                                ailmentImageDetail.Image = ailmentImage.Image;
                                ailmentImageDetail.CreatedBy = patientRequestDTO.CreatedBy;
                                ailmentImageDetail.UpdateBy = patientRequestDTO.CreatedBy;
                                ailmentImageDetail.CreatedAt = DateTime.Now;
                                ailmentImageDetail.UpdatedAt = DateTime.Now;

                                await _context.AilmentImageDetails.AddAsync(ailmentImageDetail);
                                await _context.SaveChangesAsync();

                                ailmentImage.AilmentImageId = ailmentImageDetail.AilmentImageId;
                            }
                        }
                    }

                    responseDTO.Data = ailmentMst;
                    responseDTO.Message = "Patient Details Saved Successfully!";
                    responseDTO.StatusCode = HttpStatusCode.OK;
                }
                else
                {
                    responseDTO.Data = patientRequestDTO;
                    responseDTO.Message = "No Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        public async Task<ResponseDTO> DeletePatientSymptomAsync(int SymptomId, int AilmentId)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                if (SymptomId != 0 && AilmentId != 0)
                {
                    var patientSymptomDetail = await _context.PatientSymptomDetails.Where(x => x.SymptomId == SymptomId && x.AilmentId == AilmentId).SingleOrDefaultAsync();
                    if (patientSymptomDetail != null)
                    {
                        _context.PatientSymptomDetails.Remove(patientSymptomDetail);
                        _context.SaveChanges();

                        responseDTO.Data = patientSymptomDetail;
                        responseDTO.Message = "Patient Symptoms Details Deleted!";
                        responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
                    }
                    else
                    {
                        responseDTO.Data = null;
                        responseDTO.Message = "No Patient Details Found!";
                        responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
                    }
                }
                else
                {
                    responseDTO.Data = null;
                    responseDTO.Message = "SymptomId or AilmentId are not correct!";
                    responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex.ToString();
                responseDTO.Message = ex.Message;
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }

        /*public async Task<ResponseDTO> AddUpdatePatientAsync(PatientRequestDTO patientRequestDTO)
        {
            ResponseDTO responseDTO = new ResponseDTO();
            try
            {
                if (patientRequestDTO != null)
                {
                    if (patientRequestDTO.PatientId != 0)
                    {
                        PatientDetail patientDetail = await _context.PatientDetails.Where(x => x.PatientId == patientRequestDTO.PatientId).SingleOrDefaultAsync();
                        if (patientDetail != null)
                        {
                            patientDetail.UserId = patientRequestDTO.UserId;
                            patientDetail.FirstName = patientRequestDTO.FirstName;
                            patientDetail.LastName = patientRequestDTO.LastName;
                            patientDetail.Gender = patientRequestDTO.Gender;
                            patientDetail.Dob = patientRequestDTO.Dob;
                            patientDetail.Email = patientRequestDTO.Email;
                            patientDetail.MobileNo = patientRequestDTO.MobileNo;
                            patientDetail.AlternateMobileNo = patientRequestDTO.AlternateMobileNo;
                            patientDetail.Address = patientRequestDTO.Address;
                            patientDetail.StreetLandMark = patientRequestDTO.StreetLandMark;
                            patientDetail.State = patientRequestDTO.State;
                            patientDetail.City = patientRequestDTO.City;
                            patientDetail.Pincode = patientRequestDTO.Pincode;
                            patientDetail.Image = patientRequestDTO.Image;
                            patientDetail.ImagePath = patientRequestDTO.ImagePath;
                            patientDetail.IsActive = patientRequestDTO.IsActive;
                            patientDetail.IsDelete = patientRequestDTO.IsDelete;
                            patientDetail.UpdateBy = patientRequestDTO.CreatedBy;
                            patientDetail.UpdatedAt = DateTime.Now;

                            _context.Entry(patientDetail).State = EntityState.Modified;
                            _context.SaveChanges();

                            responseDTO.Data = patientRequestDTO;
                            responseDTO.Message = "Data Updated Successfully!";
                            responseDTO.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            responseDTO.Data = patientRequestDTO;
                            responseDTO.Message = "No Data Found!";
                            responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
                        }

                    }
                    else
                    {
                        UserMst userMst = new UserMst();
                        userMst.RoleId = 2; // Patient RoleId
                        userMst.UserName = patientRequestDTO.Email;
                        userMst.Password = patientRequestDTO.FirstName.Substring(0, 3).ToUpper() + patientRequestDTO.Dob?.ToString("ddmm");
                        userMst.IsActive = patientRequestDTO.IsActive;
                        userMst.IsDeleted = patientRequestDTO.IsDelete;
                        userMst.CreateBy = patientRequestDTO.CreatedBy;
                        userMst.UpdatedBy = patientRequestDTO.CreatedBy;
                        userMst.CreateAt = DateTime.Now;
                        userMst.UpdatedAt = DateTime.Now;

                        _context.UserMsts.Add(userMst);
                        _context.SaveChanges();

                        PatientDetail patientDetail = new PatientDetail();
                        patientDetail.PatientCode = GeneratePatientUniqueCode(patientRequestDTO.FirstName, patientRequestDTO.LastName);
                        patientDetail.UserId = userMst.UserId;
                        patientDetail.FirstName = patientRequestDTO.FirstName;
                        patientDetail.LastName = patientRequestDTO.LastName;
                        patientDetail.Gender = patientRequestDTO.Gender;
                        patientDetail.Dob = patientRequestDTO.Dob;
                        patientDetail.Email = patientRequestDTO.Email;
                        patientDetail.MobileNo = patientRequestDTO.MobileNo;
                        patientDetail.AlternateMobileNo = patientRequestDTO.AlternateMobileNo;
                        patientDetail.Address = patientRequestDTO.Address;
                        patientDetail.StreetLandMark = patientRequestDTO.StreetLandMark;
                        patientDetail.State = patientRequestDTO.State;
                        patientDetail.City = patientRequestDTO.City;
                        patientDetail.Pincode = patientRequestDTO.Pincode;
                        patientDetail.Image = patientRequestDTO.Image;
                        patientDetail.ImagePath = patientRequestDTO.ImagePath;
                        patientDetail.IsActive = patientRequestDTO.IsActive;
                        patientDetail.IsDelete = patientRequestDTO.IsDelete;
                        patientDetail.CreatedBy = patientRequestDTO.CreatedBy;
                        patientDetail.UpdateBy = patientRequestDTO.CreatedBy;
                        patientDetail.CreatedAt = DateTime.Now;
                        patientDetail.UpdatedAt = DateTime.Now;

                        _context.PatientDetails.Add(patientDetail);
                        _context.SaveChanges();



                        responseDTO.Data = patientRequestDTO;
                        responseDTO.Message = "Data Added Successfully!";
                        responseDTO.StatusCode = HttpStatusCode.OK;
                    }

                }
                else
                {
                    responseDTO.Data = patientRequestDTO;
                    responseDTO.Message = "No Data Found!";
                    responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
                }
            }
            catch (Exception ex)
            {
                responseDTO.Data = ex;
                responseDTO.Message = "Exception";
                responseDTO.StatusCode = HttpStatusCode.ExpectationFailed;
            }
            return responseDTO;
        }*/

        public string GeneratePatientUniqueCode(string FirstName, string LastName)
        {
            string finalCode = "";
            finalCode = FirstName.Substring(0, 1).ToUpper() + LastName.Substring(0, 1).ToUpper();
            DateTime dt = DateTime.Now;
            finalCode = finalCode + dt.ToString("ddMMyyhhmmss");
            return finalCode;
        }
    }
}
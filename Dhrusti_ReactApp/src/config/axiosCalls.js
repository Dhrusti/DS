import axios from "axios";
import config from "./apiEndPoints";

export const APICall = async (apiEndPoint, payload) =>{
    try{
        return await axios.post(apiEndPoint, payload );
    } catch (error) {
        if (!error?.response) {
            return ({status: false, message :'Server is not responding!'});
        } else if (error.response?.status === 400) {
            return ({status: false, message :'Invalid data passed!'});
        } else if (error.response?.status === 401) {
            return ({status: false, message :'Unauthorized request!'});
        } else {
            return ({status: false, message :'Something went wrong! Please try again!'});
        }
    }
}

export const LoginAsync = async (payload) => {
    return await APICall(config.default.LoginAsync, payload);
};

export const LoginOutAsync = async (payload) => {    
    return await APICall(config.default.LogoutAsync, payload);
};

export const GetLevelTypeAsync = async (payload) => {    
    return await APICall(config.default.GetLevelTypeAsync, payload);
};

export const GenerateTypeCodeAsync = async (payload) => {    
    return await APICall(config.default.GenerateTypeCodeAsync, payload);
};

export const AddAllLevelAsync = async (payload) => {    
    return await APICall(config.default.AddAllLevelAsync, payload);
};
export const BaseURL = "http://192.168.1.143:1289/api/"; //Preyansi
//export const BaseURL = "http://192.168.1.151:8026/api/"; //Dhrusti
//export const BaseURL = "http://192.168.29.152:2326/api/"; //Tanmay

export default {
    default: {
        //Auth
        LoginAsync: BaseURL + "Auth/LoginAsync",
        LogoutAsync: BaseURL + "Auth/LogoutAsync",

        //Levels
        AddAllLevelAsync: BaseURL + "Levels/AddAllLevelAsync",
        GenerateTypeCodeAsync: BaseURL + "Levels/GenerateTypeCodeAsync",
        GetLevelTypeAsync: BaseURL + "Levels/GetLevelTypeAsync"
    }
}
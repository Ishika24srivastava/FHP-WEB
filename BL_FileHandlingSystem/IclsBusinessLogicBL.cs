using FIleHandlingSystem.VO;

namespace FileHandlingSystem.BL
{
    public interface IclsBusinessLogicBL
    {
        bool CheckValidUser(AuthenticatorValueObject vo);
        bool Save(clsEmployeeVO vo);
        bool SetConfigurations();
    }
}
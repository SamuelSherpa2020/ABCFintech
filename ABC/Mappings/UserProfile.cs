using ABC.Dtos.BankDto;
using ABC.Dtos.PaymentDto;
using ABC.Dtos.TransactionDto;
using ABC.Dtos.UserBankDto;
using ABC.Dtos.UserDto;
using ABC.Models;
using AutoMapper;

namespace ABC.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Map User to CreateUserDto
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, GetUserDto>().ReverseMap();


            // mapping Payment
            CreateMap<PaymentDetail, CreatePaymentDto>().ReverseMap();
            CreateMap<PaymentDetail,GetPaymentDto>().ReverseMap();

            // mapping Transaction
            CreateMap<Transaction, CreateTransactionDto>().ReverseMap();
            CreateMap<Transaction, GetTransactionDto>().ReverseMap();

            // mapping Bank
            CreateMap<Bank, CreateBankDto>().ReverseMap();
            CreateMap<Bank, GetBankDto>().ReverseMap();


            // mapping UserBank
            CreateMap<UserBank, CreateUserBankDto>().ReverseMap();
            CreateMap<UserBank, GetUserBankDto>().ReverseMap();

        }

    }
}

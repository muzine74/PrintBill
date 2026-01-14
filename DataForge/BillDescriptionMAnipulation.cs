using DataBridge.Entity;
using DBConnection;
using DBConnection.Entity;
using Microsoft.EntityFrameworkCore;
//using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace DataBridge
{
    public class BillDescriptionMAnipulation
    {
        DbContextOptions<RamssisCleaningContex> options;
        RamssisCleaningContex ramssisCleaningContex;
        BillDescriptionPoco billDescriptionPoco;
        MapperConfiguration config;
        //IMapper mapper;

        IMapper _mapper;

        public BillDescriptionMAnipulation()
        {
            options = new DbContextOptionsBuilder<RamssisCleaningContex>()
            .UseSqlServer("Server=DESKTOP-71ON71H\\SQLEXPRESS;Database=RamssisCleaningDB;Trusted_Connection=True;TrustServerCertificate=true;")
.Options;

            ramssisCleaningContex = new RamssisCleaningContex(options);
            billDescriptionPoco = new BillDescriptionPoco();




            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DBConnection.Entity.BillDescription, DataBridge.Entity.BillDescriptionPoco>()
                .ForMember(billDescriptionPoco => billDescriptionPoco.BillDescriptionPocoId, opt => opt.MapFrom(billDescription => billDescription.BillDescriptionId))
                .ForMember(billDescriptionPoco => billDescriptionPoco.BillHistoryIdPoco, opt => opt.MapFrom(billDescription => billDescription.BillHistoryId))
                .ForMember(billDescriptionPoco => billDescriptionPoco.QuantityPoco, opt => opt.MapFrom(billDescription => billDescription.Quantity))
                .ForMember(billDescriptionPoco => billDescriptionPoco.DescriptionPoco, opt => opt.MapFrom(billDescription => billDescription.Description))
                .ForMember(billDescriptionPoco => billDescriptionPoco.UnitPricePoco, opt => opt.MapFrom(billDescription => billDescription.UnitPrice))
                .ForMember(billDescriptionPoco => billDescriptionPoco.SubTotalPricePoco, opt => opt.MapFrom(billDescription => billDescription.SubTotalPrice));
            });

            
            _mapper = config.CreateMapper();

        }

        public List<BillDescriptionPoco> GetBillDescriptionByBillHistoryId(int bilIdentifier)
        {
            var billDescriptionList = ramssisCleaningContex.BillDescriptions
        .Where(b => b.BillHistoryId == bilIdentifier)
        .ToList();

            return _mapper.Map<List<BillDescriptionPoco>>(billDescriptionList);
        }   

    }
}

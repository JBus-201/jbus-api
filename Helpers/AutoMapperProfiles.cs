using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Admin, AdminDto>();
            CreateMap<AdminCreateDto, Admin>();
            CreateMap<AdminUpdateDto, Admin>()
            .ForMember(p => p.UserId, opt => opt.Ignore())
            .ForMember(p => p.User, opt => opt.Ignore());

            CreateMap<Bus, BusDto>()
            .ForMember(bd => bd.Going, opt => opt.MapFrom(b => b.Going.ToString()));
            CreateMap<BusCreateDto, Bus>();
            CreateMap<BusUpdateDto, Bus>()
            .ForMember(bus => bus.BusNumber, opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.BusNumber)))
            .ForMember(bus => bus.Capacity, opt => opt.PreCondition(src => src.Capacity > 0))
            .ForMember(bus => bus.UpdatedAt, opt => opt.PreCondition(src => src.UpdatedAt != default))
            .ForMember(bus => bus.RouteId, opt => opt.PreCondition(src => src.RouteId != default))
            .ForMember(bus => bus.DriverId, opt => opt.PreCondition(src => src.DriverId != default));

            CreateMap<ChargingTransaction, ChargingTransactionDto>()
            .ForMember(ctd => ctd.chargingMethod, opt => opt.MapFrom(ct => ct.ChargingMethod.ToString()));
            CreateMap<ChargingTransactionCreateDto, ChargingTransaction>();

            CreateMap<Driver, DriverDto>();
            CreateMap<DriverCreateDto, Driver>();
            CreateMap<DriverUpdateDto, Driver>()
            .ForMember(p => p.UserId, opt => opt.Ignore())
            .ForMember(p => p.User, opt => opt.Ignore());

            CreateMap<DriverTrip, DriverTripDto>()
            .ForMember(dtd => dtd.Route, opt => opt.MapFrom(dt => dt.Route))
            .ForMember(dtd => dtd.status, opt => opt.MapFrom(dt => dt.status.ToString()));
            CreateMap<DriverTripCreateDto, DriverTrip>();
            CreateMap<DriverTripUpdateDto, DriverTrip>()
            .ForMember(dtc => dtc.status, opt => opt.MapFrom(dt => Enum.Parse<TripStatus>(dt.status!)))
            // .ForMember(dtc => dtc.Rating, opt => opt.PreCondition(src => src.Rating != default))
            .ForMember(dtc => dtc.status, opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.status)));

            CreateMap<FavoritePoint, FavoritePointDto>();

            CreateMap<Fazaa, FazaaDto>();
            CreateMap<FazaaCreateDto, Fazaa>();
            CreateMap<FazaaUpdateDto, Fazaa>();

            CreateMap<Friends, FriendsDto>();
            CreateMap<FriendsCreateDto, Friends>();
            CreateMap<Passenger, FriendDto>();

            CreateMap<InterestPoint, InterestPointDto>();
            CreateMap<InterestPointCreateDto, InterestPoint>();
            CreateMap<InterestPointUpdateDto, InterestPoint>();

            CreateMap<OTP, OTPDto>();

            CreateMap<Passenger, PassengerDto>();
            CreateMap<PassengerUpdateDto, Passenger>()
            .ForMember(p => p.UserId, opt => opt.Ignore())
            .ForMember(p => p.User, opt => opt.Ignore())
            .ForMember(p => p.Creditors, opt => opt.Ignore())
            .ForMember(p => p.InDebts, opt => opt.Ignore())
            .ForMember(p => p.ProfileImage, opt => opt.PreCondition(src => src.ProfileImage != null));

            CreateMap<PaymentTransaction, PaymentTransactionDto>();
            CreateMap<PaymentTransactionCreateDto, PaymentTransaction>();

            CreateMap<Point, PointDto>();
            CreateMap<PointCreateDto, Point>();
            CreateMap<PointUpdateDto, Point>();

            CreateMap<PredefinedStops, PredefinedStopsDto>();
            CreateMap<PredefinedStopsCreateDto, PredefinedStops>();

            CreateMap<Entities.Route, RouteDto>()
            .ForMember(r => r.PredefinedStops, opt => opt.MapFrom(r => r.PredefinedStops))
            .ForMember(r => r.PredefinedStops, opt => opt.PreCondition(src => src.PredefinedStops != null));
            CreateMap<RouteCreateDto, Entities.Route>();
            CreateMap<RouteUpdateDto, Entities.Route>()
            .ForMember(r => r.Name, opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.Name)))
            .ForMember(r => r.Fee, opt => opt.PreCondition(src => src.Fee > 0))
            .ForMember(r => r.WaypointsGoing, opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.WaypointsGoing)))
            .ForMember(r => r.WaypointsReturning, opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.WaypointsReturning)))
            .ForMember(r => r.UpdatedAt, opt => opt.PreCondition(src => src.UpdatedAt != default))
            .ForMember(r => r.StartingPoint, opt => opt.PreCondition(src => src.StartingPoint != null))
            .ForMember(r => r.EndingPoint, opt => opt.PreCondition(src => src.EndingPoint != null));

            CreateMap<ScratchCard, ScratchCardDto>()
            .ForMember(scd => scd.Status, opt => opt.MapFrom(sc => sc.Status.ToString()))
            .ForMember(scd => scd.Type, opt => opt.MapFrom(sc => sc.Type.ToString()));
            CreateMap<ScratchCardCreateDto, ScratchCard>()
            .ForMember(scd => scd.Type, opt => opt.MapFrom(sc => Enum.Parse<ScratchCardType>(sc.Type!)));


            CreateMap<Trip, TripDto>()
            .ForMember(td => td.Status, opt => opt.MapFrom(t => t.status.ToString()));
            CreateMap<TripUpdateDto, Trip>()
            .ForMember(t => t.PaymentTransactionId, opt => opt.PreCondition(src => src.PaymentTransactionId != default))
            .ForMember(t => t.PickUpPoint, opt => opt.PreCondition(src => src.PickUpPoint != null))
            .ForMember(t => t.DropOffPoint, opt => opt.PreCondition(src => src.DropOffPoint != null));
            CreateMap<TripCreateDto, Trip>();


            CreateMap<User, UserDto>()
            .ForMember(ud => ud.Sex, opt => opt.MapFrom(u => u.Sex.ToString()))
            .ForMember(ud => ud.Role, opt => opt.MapFrom(u => u.Role.ToString()));
            CreateMap<UserUpdateDto, User>()
            .ForMember(u => u.Sex, opt => opt.MapFrom(uud => Enum.Parse<Sex>(uud.Sex!)))
            .ForMember(u => u.Name, opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.Name)))
            .ForMember(u => u.Email, opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.Email)))
            .ForMember(u => u.Sex, opt => opt.PreCondition(src => !string.IsNullOrEmpty(src.Sex)));
            CreateMap<UserDto, User>();
        }
    }
}

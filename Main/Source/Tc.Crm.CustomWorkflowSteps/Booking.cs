﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tc.Crm.CustomWorkflowSteps
{
    //public Accommodation AccommdationkInfo { get; set; }




    //------------------------------------------------------------------------------
    // <auto-generated>
    //     This code was generated by a tool.
    //     Runtime Version:4.0.30319.42000
    //
    //     Changes to this file may cause incorrect behavior and will be lost if
    //     the code is regenerated.
    // </auto-generated>
    //------------------------------------------------------------------------------

    [DataContract(Name = "booking")]
    public class Booking
    {

        [DataMember(Name = "bookingIdentifier")]
        public BookingIdentifier BookingIdentifier { get; set; }

        [DataMember(Name = "bookingGeneral")]
        public BookingGeneral BookingGeneral { get; set; }

        [DataMember(Name = "bookingIdentity")]
        public BookingIdentity BookingIdentity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "travelParticipant")]
        public TravelParticipant[] TravelParticipant { get; set; }

        [DataMember(Name = "services")]
        public BookingServices Services { get; set; }

        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "remark")]
        public Remark[] Remark { get; set; }
    }

    [DataContract(Name = "address")]
    public class Address
    {

        [DataMember(Name = "additionalAddressInfo")]
        public string AdditionalAddressInfo { get; set; }

        [DataMember(Name = "flatNumberUnit")]
        public string FlatNumberUnit { get; set; }

        [DataMember(Name = "houseNumberBuilding")]
        public string HouseNumberBuilding { get; set; }

        [DataMember(Name = "box")]
        public string Box { get; set; }

        [DataMember(Name = "town")]
        public string Town { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "county")]
        public string County { get; set; }

        [DataMember(Name = "number")]
        public string Number { get; set; }

        [DataMember(Name = "postalCode")]
        public string PostalCode { get; set; }

        [DataMember(Name = "street")]
        public string Street { get; set; }

        [DataMember(Name = "type")]
        public AddressType AddressType { get; set; }
    }

    [DataContract(Name = "AddressType")]
    public enum AddressType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "M")]
        [EnumMember]
        M
    }

    [DataContract(Name = "BookingStatus")]
    public enum BookingStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B")]
        [EnumMember]
        B,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "C")]
        [EnumMember]
        C
    }

    [DataContract(Name = "Gender")]
    public enum Gender
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "M")]
        [EnumMember]
        M,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "F")]
        [EnumMember]
        F,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "U")]
        [EnumMember]
        U
    }
    [DataContract(Name = "Relation")]
    public enum Relation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "P")]
        [EnumMember]
        P,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "C")]
        [EnumMember]
        C,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "I")]
        [EnumMember]
        I
    }
    [DataContract(Name = "BoardType")]
    public enum BoardType
    {
        [EnumMember]
        AI,
        [EnumMember]
        HB
    }

    [DataContract(Name = "AccomodationStatus")]
    public enum AccommodationStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "OK")]
        [EnumMember]
        OK,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "R")]
        [EnumMember]
        R,
        [EnumMember]
        PR
    }
    [DataContract(Name = "TransferType")]
    public enum TransferType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "I")]
        [EnumMember]
        I,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "O")]
        [EnumMember]
        O,
        [EnumMember]
        TH
    }
    [DataContract(Name = "CustomerStatus")]
    public enum CustomerStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A")]
        [EnumMember]
        A,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "D")]
        [EnumMember]
        D,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B")]
        [EnumMember]
        B
    }
    [DataContract(Name = "CustomerType")]
    public enum CustomerType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "P")]
        [EnumMember]
        P,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "B")]
        [EnumMember]
        B
    }
    [DataContract(Name = "PhoneType")]
    public enum PhoneType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "H")]
        [EnumMember]
        H,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "M")]
        [EnumMember]
        M
    }
    [DataContract(Name = "EmailType")]
    public enum EmailType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Pri")]
        [EnumMember]
        Pri,
        [EnumMember]
        Pro
    }
    [DataContract(Name = "RemarkType")]
    public enum RemarkType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "T")]
        [EnumMember]
        T,
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "A")]
        [EnumMember]
        A
    }

    [DataContract(Name = "bookingIdentifier")]
    public class BookingIdentifier
    {

        [DataMember(Name = "sourceMarket")]
        public string SourceMarket { get; set; }

        [DataMember(Name = "sourceSystem")]
        public string SourceSystem { get; set; }

        [DataMember(Name = "bookingNumber")]
        public string BookingNumber { get; set; }

        [DataMember(Name = "bookingVersionOnTour")]
        public string BookingVersionOnTour { get; set; }

        [DataMember(Name = "bookingVersionTourOperator")]
        public string BookingVersionTourOperator { get; set; }

        [DataMember(Name = "bookingUpdateDateOnTour")]
        public string BookingUpdateDateOnTour { get; set; }

        [DataMember(Name = "bookingUpdateDateTourOperator")]
        public string BookingUpdateDateTourOperator { get; set; }
    }

    [DataContract(Name = "bookingGeneral")]
    public class BookingGeneral
    {

        [DataMember(Name = "bookingStatus")]
        public BookingStatus BookingStatus { get; set; }

        [DataMember(Name = "bookingDate")]
        public string BookingDate { get; set; }

        [DataMember(Name = "departureDate")]
        public string DepartureDate { get; set; }

        [DataMember(Name = "returnDate")]
        public string ReturnDate { get; set; }

        [DataMember(Name = "duration")]
        public string Duration { get; set; }

        [DataMember(Name = "destination")]
        public string Destination { get; set; }

        [DataMember(Name = "toCode")]
        public string ToCode { get; set; }

        [DataMember(Name = "brand")]
        public string Brand { get; set; }

        [DataMember(Name = "brochureCode")]
        public string BrochureCode { get; set; }

        [DataMember(Name = "isLateBooking")]
        public bool IsLateBooking { get; set; }

        [DataMember(Name = "numberofParticipants")]
        public int NumberOfParticipants { get; set; }

        [DataMember(Name = "numberOfAdults")]
        public int NumberOfAdults { get; set; }

        [DataMember(Name = "numberOfChildren")]
        public int NumberOfChildren { get; set; }

        [DataMember(Name = "numberOfInfants")]
        public int NumberOfInfants { get; set; }

        [DataMember(Name = "travelAmount")]
        public decimal TravelAmount { get; set; }

        [DataMember(Name = "currency")]
        public string Currency { get; set; }

        [DataMember(Name = "hasComplaint")]
        public bool HasComplaint { get; set; }
    }

    [DataContract(Name = "bookingIdentity")]
    public class BookingIdentity
    {

        [DataMember(Name = "booker")]
        public BookingBooker Booker { get; set; }
    }

    [DataContract(Name = "Remark")]
    public class Remark
    {

        [DataMember(Name = "type")]
        public RemarkType RemarkType { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }
    }

    [DataContract(Name = "travelParticipant")]
    public class TravelParticipant
    {

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "age")]
        public int Age { get; set; }

        [DataMember(Name = "gender")]
        public Gender Gender { get; set; }

        [DataMember(Name = "relation")]
        public Relation Relation { get; set; }

        [DataMember(Name = "travelParticipantIDOnTour")]
        public string TravelParticipantIdOnTour { get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "birthDate")]
        public string Birthdate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "Remark")]
        public Remark[] Remark { get; set; }
    }

    [DataContract(Name = "travelParticipantAssignment")]
    public class TravelParticipantAssignment
    {

        [DataMember(Name = "travelParticipantID")]
        public string TravelParticipantId { get; set; }
    }

    [DataContract(Name = "brands")]
    public class Brands
    {
    }

    [DataContract(Name = "tourguide")]
    public class TourGuide
    {

        [DataMember(Name = "tourguideID")]
        public string TourGuideId { get; set; }

        [DataMember(Name = "tourguideName")]
        public string TourGuideName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "brands")]
        public Brands[] Brands { get; set; }
    }

    [DataContract(Name = "tourguideAssignment")]
    public class TourGuideAssignment
    {

        [DataMember(Name = "tourguide")]
        public TourGuide TourGuide { get; set; }
    }

    [DataContract(Name = "accommodation")]
    public class Accommodation
    {

        [DataMember(Name = "accommodationCode")]
        public string AccommodationCode { get; set; }

        [DataMember(Name = "groupAccommodationCode")]
        public string GroupAccommodationCode { get; set; }

        [DataMember(Name = "accommodationDescription")]
        public string AccommodationDescription { get; set; }

        [DataMember(Name = "order")]
        public int Order { get; set; }

        [DataMember(Name = "startDate")]
        public string StartDate { get; set; }

        [DataMember(Name = "endDate")]
        public string EndDate { get; set; }

        [DataMember(Name = "roomType")]
        public string RoomType { get; set; }

        [DataMember(Name = "boardType")]
        public BoardType BoardType { get; set; }

        [DataMember(Name = "status")]
        public AccommodationStatus Status { get; set; }

        [DataMember(Name = "hasSharedRoom")]
        public bool HasSharedRoom { get; set; }

        [DataMember(Name = "numberOfParticipants")]
        public int NumberOfParticipants { get; set; }

        [DataMember(Name = "numberOfRooms")]
        public int NumberOfRooms { get; set; }

        [DataMember(Name = "withTransfer")]
        public bool WithTransfer { get; set; }

        [DataMember(Name = "isExternalService")]
        public bool IsExternalService { get; set; }

        [DataMember(Name = "externalServiceCode")]
        public string ExternalServiceCode { get; set; }

        [DataMember(Name = "notificationRequired")]
        public bool NotificationRequired { get; set; }

        [DataMember(Name = "needsTourGuideAssignment")]
        public bool NeedsTourGuideAssignment { get; set; }

        [DataMember(Name = "isExternalTransfer")]
        public bool IsExternalTransfer { get; set; }

        [DataMember(Name = "transferServiceLevel")]
        public string TransferServiceLevel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "travelParticipantAssignment")]
        public TravelParticipantAssignment[] TravelParticipantAssignment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "remark")]
        public Remark[] Remark { get; set; }

        [DataMember(Name = "tourguideAssignment")]
        public TourGuideAssignment TourGuideAssignment { get; set; }
    }

    [DataContract(Name = "transport")]
    public class Transport
    {

        [DataMember(Name = "transportCode")]
        public string TransportCode { get; set; }

        [DataMember(Name = "transportDescription")]
        public string TransportDescription { get; set; }

        [DataMember(Name = "order")]
        public int Order { get; set; }

        [DataMember(Name = "startDate")]
        public string StartDate { get; set; }

        [DataMember(Name = "endDate")]
        public string EndDate { get; set; }

        [DataMember(Name = "transferType")]
        public TransferType TransferType { get; set; }

        [DataMember(Name = "departureAirport")]
        public string DepartureAirport { get; set; }

        [DataMember(Name = "arrivalAirport")]
        public string ArrivalAirport { get; set; }

        [DataMember(Name = "carrierCode")]
        public string CarrierCode { get; set; }

        [DataMember(Name = "flightNumber")]
        public string FlightNumber { get; set; }

        [DataMember(Name = "flightIdentifier")]
        public string FlightIdentifier { get; set; }

        [DataMember(Name = "numberOfParticipants")]
        public int NumberOfParticipants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "travelParticipantAssignment")]
        public TravelParticipantAssignment[] TravelParticipantAssignment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "remark")]
        public Remark[] Remark { get; set; }
    }

    [DataContract(Name = "transfer")]
    public class Transfer
    {

        [DataMember(Name = "transferCode")]
        public string TransferCode { get; set; }

        [DataMember(Name = "transferDescription")]
        public string TransferDescription { get; set; }

        [DataMember(Name = "order")]
        public int Order { get; set; }

        [DataMember(Name = "startDate")]
        public string StartDate { get; set; }

        [DataMember(Name = "category")]
        public string Category { get; set; }

        [DataMember(Name = "endDate")]
        public string EndDate { get; set; }

        [DataMember(Name = "transferType")]
        public TransferType TransferType { get; set; }

        [DataMember(Name = "departureAirport")]
        public string DepartureAirport { get; set; }

        [DataMember(Name = "arrivalAirport")]
        public string ArrivalAirport { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "travelParticipantAssignment")]
        public TravelParticipantAssignment[] TravelParticipantAssignment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "remark")]
        public Remark[] Remark { get; set; }
    }

    [DataContract(Name = "extraServiceCode")]
    public class ExtraServiceCode
    {
    }

    [DataContract(Name = "extraServiceDescription")]
    public class ExtraServiceDescription
    {
    }

    [DataContract(Name = "extraService")]
    public class ExtraService
    {

        [DataMember(Name = "extraServiceCode")]
        public ExtraServiceCode ExtraServiceCode { get; set; }

        [DataMember(Name = "extraServiceDescription")]
        public ExtraServiceDescription ExtraServiceDescription { get; set; }

        [DataMember(Name = "order")]
        public int Order { get; set; }

        [DataMember(Name = "startDate")]
        public string StartDate { get; set; }

        [DataMember(Name = "endDate")]
        public string EndDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "travelParticipantAssignment")]
        public TravelParticipantAssignment[] TravelParticipantAssignment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "remark")]
        public Remark[] Remark { get; set; }
    }

    [DataContract(Name = "services")]
    public class BookingServices
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "accommodation")]
        public Accommodation[] Accommodation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "transport")]
        public Transport[] Transport { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "transfer")]
        public Transfer[] Transfer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "extraService")]
        public ExtraService[] ExtraService { get; set; }
    }

    [DataContract(Name = "customerIdentifier")]
    public class CustomerIdentifier
    {

        [DataMember(Name = "customerID")]
        public string CustomerId { get; set; }

        [DataMember(Name = "businessArea")]
        public string BusinessArea { get; set; }

        [DataMember(Name = "sourceMarket")]
        public string SourceMarket { get; set; }

        [DataMember(Name = "sourceSystem")]
        public string SourceSystem { get; set; }
    }

    [DataContract(Name = "customerGeneral")]
    public class CustomerGeneral
    {

        [DataMember(Name = "customerStatus")]
        public CustomerStatus CustomerStatus { get; set; }

        [DataMember(Name = "customerType")]
        public CustomerType CustomerType { get; set; }
    }

    [DataContract(Name = "customerIdentity")]
    public class CustomerIdentity
    {

        [DataMember(Name = "salutation")]
        public string Salutation { get; set; }

        [DataMember(Name = "academictitle")]
        public string AcademicTitle { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "middleName")]
        public string MiddleName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "gender")]
        public Gender Gender { get; set; }

        [DataMember(Name = "birthdate")]
        public string Birthdate { get; set; }
    }

    [DataContract(Name = "company")]
    public class Company
    {

        [DataMember(Name = "companyName")]
        public string CompanyName { get; set; }
    }

    [DataContract(Name = "additional")]
    public class Additional
    {

        [DataMember(Name = "segment")]
        public string Segment { get; set; }

        [DataMember(Name = "dateOfdeath")]
        public string DateOfDeath { get; set; }
    }

    [DataContract(Name = "phone")]
    public class Phone
    {

        [DataMember(Name = "type")]
        public PhoneType PhoneType { get; set; }

        [DataMember(Name = "number")]
        public string Number { get; set; }
    }

    [DataContract(Name = "email")]
    public class Email
    {

        [DataMember(Name = "type")]
        public EmailType EmailType { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }
    }

    [DataContract(Name = "social")]
    public class Social
    {

        [DataMember(Name = "type")]
        public string SocialType { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }

    [DataContract(Name = "customer")]
    public class Customer
    {

        [DataMember(Name = "customerIdentifier")]
        public CustomerIdentifier CustomerIdentifier { get; set; }

        [DataMember(Name = "customerGeneral")]
        public CustomerGeneral CustomerGeneral { get; set; }

        [DataMember(Name = "customerIdentity")]
        public CustomerIdentity CustomerIdentity { get; set; }

        [DataMember(Name = "company")]
        public Company Company { get; set; }

        [DataMember(Name = "additional")]
        public Additional Additional { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "address")]
        public Address[] Address { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "phone")]
        public Phone[] Phone { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "email")]
        public Email[] Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        [DataMember(Name = "social")]
        public Social[] Social { get; set; }
    }      

    [DataContract(Name = "booking_booker")]
    public class BookingBooker
    {
        [DataMember(Name = "address")]
        public Address Address { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "phone")]
        public string Phone { get; set; }
        [DataMember(Name = "mobile")]
        public string Mobile { get; set; }
        [DataMember(Name = "emergencyNumber")]
        public string EmergencyNumber { get; set; }
    }
}

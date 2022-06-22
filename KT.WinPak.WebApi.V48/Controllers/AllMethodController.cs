using KT.Common.WebApi.HttpApi;
using KT.Common.Core.Utils;
using KT.WinPak.Data.V48.Models;
using KT.WinPak.SDK.V48;
using KT.WinPak.SDK.V48.IServices;
using KT.WinPak.SDK.V48.Models;
using KT.WinPak.SDK.V48.Queries;
using KT.WinPak.SDK.V48.Services;
using KT.WinPak.Service.V48.IServices;
using KT.WinPak.Service.V48.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace KT.WinPak.WebApi.V48.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllMethodController : ControllerBase
    {
        private ILogger<AllMethodController> _logger;
        private IAllSdkService _allSdkService;

        public AllMethodController(ILogger<AllMethodController> logger, IAllSdkService allSdkService)
        {
            _logger = logger;
            _allSdkService = allSdkService;
        }
        [HttpPost("Login")]
        public LoginQuery Login([FromBody]LoginQuery query)
        {
            var result = _allSdkService.Login(query);
            return result;
        }
        [HttpPost("GetAccounts")]
        public GetAccountsQuery GetAccounts([FromBody]GetAccountsQuery query)
        {
            var result = _allSdkService.GetAccounts(query);
            return result;
        }
        [HttpPost("GetCardsByAccountName")]
        public GetCardsByAccountNameQuery GetCardsByAccountName([FromBody]GetCardsByAccountNameQuery query)
        {
            var result = _allSdkService.GetCardsByAccountName(query);
            return result;
        }
        [HttpPost("GetAccessLevelsByAccountName")]
        public GetAccessLevelsByAccountNameQuery GetAccessLevelsByAccountName([FromBody]GetAccessLevelsByAccountNameQuery query)
        {
            var result = _allSdkService.GetAccessLevelsByAccountName(query);
            return result;
        }
        [HttpPost("GetMaxCardNumberLength")]
        public GetMaxCardNumberLengthQuery GetMaxCardNumberLength([FromBody]GetMaxCardNumberLengthQuery query)
        {
            var result = _allSdkService.GetMaxCardNumberLength(query);
            return result;
        }
        [HttpPost("GetAccessLevelType")]
        public GetAccessLevelTypeQuery GetAccessLevelType([FromBody]GetAccessLevelTypeQuery query)
        {
            var result = _allSdkService.GetAccessLevelType(query);
            return result;
        }
        [HttpPost("GetCardNumeric")]
        public GetCardNumericQuery GetCardNumeric([FromBody]GetCardNumericQuery query)
        {
            var result = _allSdkService.GetCardNumeric(query);
            return result;
        }
        [HttpPost("AddCard")]
        public AddCardQuery AddCard([FromBody]AddCardQuery query)
        {
            var result = _allSdkService.AddCard(query);
            return result;
        }
        [HttpPost("DeleteCard")]
        public DeleteCardQuery DeleteCard([FromBody]DeleteCardQuery query)
        {
            var result = _allSdkService.DeleteCard(query);
            return result;
        }
        [HttpPost("GetCardByCardNumber")]
        public GetCardByCardNumberQuery GetCardByCardNumber([FromBody]GetCardByCardNumberQuery query)
        {
            var result = _allSdkService.GetCardByCardNumber(query);
            return result;
        }
        [HttpPost("EditCard")]
        public EditCardQuery EditCard([FromBody]EditCardQuery query)
        {
            var result = _allSdkService.EditCard(query);
            return result;
        }
        [HttpPost("AddAccessLevel")]
        public AddAccessLevelQuery AddAccessLevel([FromBody]AddAccessLevelQuery query)
        {
            var result = _allSdkService.AddAccessLevel(query);
            return result;
        }
        [HttpPost("EditAccessLevel")]
        public EditAccessLevelQuery EditAccessLevel([FromBody]EditAccessLevelQuery query)
        {
            var result = _allSdkService.EditAccessLevel(query);
            return result;
        }
        [HttpPost("DeleteAccessLevel")]
        public DeleteAccessLevelQuery DeleteAccessLevel([FromBody]DeleteAccessLevelQuery query)
        {
            var result = _allSdkService.DeleteAccessLevel(query);
            return result;
        }
        [HttpPost("DeleteAL")]
        public DeleteALQuery DeleteAL([FromBody]DeleteALQuery query)
        {
            var result = _allSdkService.DeleteAL(query);
            return result;
        }
        [HttpPost("IsolateAccessLevel")]
        public IsolateAccessLevelQuery IsolateAccessLevel([FromBody]IsolateAccessLevelQuery query)
        {
            var result = _allSdkService.IsolateAccessLevel(query);
            return result;
        }
        [HttpPost("GetAccessLevelForReassign")]
        public GetAccessLevelForReassignQuery GetAccessLevelForReassign([FromBody]GetAccessLevelForReassignQuery query)
        {
            var result = _allSdkService.GetAccessLevelForReassign(query);
            return result;
        }
        [HttpPost("ReassignAccessLevel")]
        public ReassignAccessLevelQuery ReassignAccessLevel([FromBody]ReassignAccessLevelQuery query)
        {
            var result = _allSdkService.ReassignAccessLevel(query);
            return result;
        }
        [HttpPost("ConfigureAccessLevel")]
        public ConfigureAccessLevelQuery ConfigureAccessLevel([FromBody]ConfigureAccessLevelQuery query)
        {
            var result = _allSdkService.ConfigureAccessLevel(query);
            return result;
        }
        [HttpPost("GetTimeZonesByAccountName")]
        public GetTimeZonesByAccountNameQuery GetTimeZonesByAccountName([FromBody]GetTimeZonesByAccountNameQuery query)
        {
            var result = _allSdkService.GetTimeZonesByAccountName(query);
            return result;
        }
        [HttpPost("GetWPDSN")]
        public GetWPDSNQuery GetWPDSN([FromBody]GetWPDSNQuery query)
        {
            var result = _allSdkService.GetWPDSN(query);
            return result;
        }
        [HttpPost("GetReadersByAccountName")]
        public GetReadersByAccountNameQuery GetReadersByAccountName([FromBody]GetReadersByAccountNameQuery query)
        {
            var result = _allSdkService.GetReadersByAccountName(query);
            return result;
        }
        [HttpPost("GetADVDetailsByAccountName")]
        public GetADVDetailsByAccountNameQuery GetADVDetailsByAccountName([FromBody]GetADVDetailsByAccountNameQuery query)
        {
            var result = _allSdkService.GetADVDetailsByAccountName(query);
            return result;
        }
        [HttpPost("GetAcctIDByHID")]
        public GetAcctIDByHIDQuery GetAcctIDByHID([FromBody]GetAcctIDByHIDQuery query)
        {
            var result = _allSdkService.GetAcctIDByHID(query);
            return result;
        }
        [HttpPost("GetAvailableTimeZonesOfReader")]
        public GetAvailableTimeZonesOfReaderQuery GetAvailableTimeZonesOfReader([FromBody]GetAvailableTimeZonesOfReaderQuery query)
        {
            var result = _allSdkService.GetAvailableTimeZonesOfReader(query);
            return result;
        }
        [HttpPost("GetAssociatedTimeZoneOfReader")]
        public GetAssociatedTimeZoneOfReaderQuery GetAssociatedTimeZoneOfReader([FromBody]GetAssociatedTimeZoneOfReaderQuery query)
        {
            var result = _allSdkService.GetAssociatedTimeZoneOfReader(query);
            return result;
        }
        [HttpPost("GetAccessAreaBranchesByAccountName")]
        public GetAccessAreaBranchesByAccountNameQuery GetAccessAreaBranchesByAccountName([FromBody]GetAccessAreaBranchesByAccountNameQuery query)
        {
            var result = _allSdkService.GetAccessAreaBranchesByAccountName(query);
            return result;
        }
        [HttpPost("GetReadersInAccessAreaBranch")]
        public GetReadersInAccessAreaBranchQuery GetReadersInAccessAreaBranch([FromBody]GetReadersInAccessAreaBranchQuery query)
        {
            var result = _allSdkService.GetReadersInAccessAreaBranch(query);
            return result;
        }
        [HttpPost("GetAvailableTimezonesOfBranch")]
        public GetAvailableTimezonesOfBranchQuery GetAvailableTimezonesOfBranch([FromBody]GetAvailableTimezonesOfBranchQuery query)
        {
            var result = _allSdkService.GetAvailableTimezonesOfBranch(query);
            return result;
        }
        [HttpPost("GetCardHoldersByAccountName")]
        public GetCardHoldersByAccountNameQuery GetCardHoldersByAccountName([FromBody]GetCardHoldersByAccountNameQuery query)
        {
            var result = _allSdkService.GetCardHoldersByAccountName(query);
            return result;
        }
        [HttpPost("GetNoteFieldTemplateDetailsByAccount")]
        public GetNoteFieldTemplateDetailsByAccountQuery GetNoteFieldTemplateDetailsByAccount([FromBody]GetNoteFieldTemplateDetailsByAccountQuery query)
        {
            var result = _allSdkService.GetNoteFieldTemplateDetailsByAccount(query);
            return result;
        }
        [HttpPost("AddCardHolder")]
        public AddCardHolderQuery AddCardHolder([FromBody]AddCardHolderQuery query)
        {
            var result = _allSdkService.AddCardHolder(query);
            return result;
        }
        [HttpPost("EditCardHolder")]
        public EditCardHolderQuery EditCardHolder([FromBody]EditCardHolderQuery query)
        {
            var result = _allSdkService.EditCardHolder(query);
            return result;
        }
        [HttpPost("DeleteCardHolder")]
        public DeleteCardHolderQuery DeleteCardHolder([FromBody]DeleteCardHolderQuery query)
        {
            var result = _allSdkService.DeleteCardHolder(query);
            return result;
        }
        [HttpPost("Logout")]
        public LogoutQuery Logout([FromBody]LogoutQuery query)
        {
            var result = _allSdkService.Logout(query);
            return result;
        }
        [HttpPost("GetAccessLevelByName")]
        public GetAccessLevelByNameQuery GetAccessLevelByName([FromBody]GetAccessLevelByNameQuery query)
        {
            var result = _allSdkService.GetAccessLevelByName(query);
            return result;
        }
        [HttpPost("GetCardHolderByCardHolderID")]
        public GetCardHolderByCardHolderIDQuery GetCardHolderByCardHolderID([FromBody]GetCardHolderByCardHolderIDQuery query)
        {
            var result = _allSdkService.GetCardHolderByCardHolderID(query);
            return result;
        }
        [HttpPost("GetAccountNameByAcctID")]
        public GetAccountNameByAcctIDQuery GetAccountNameByAcctID([FromBody]GetAccountNameByAcctIDQuery query)
        {
            var result = _allSdkService.GetAccountNameByAcctID(query);
            return result;
        }
        [HttpPost("GetAccessLevelNameByID")]
        public GetAccessLevelNameByIDQuery GetAccessLevelNameByID([FromBody]GetAccessLevelNameByIDQuery query)
        {
            var result = _allSdkService.GetAccessLevelNameByID(query);
            return result;
        }
        [HttpPost("GetCardsByCHID")]
        public GetCardsByCHIDQuery GetCardsByCHID([FromBody]GetCardsByCHIDQuery query)
        {
            var result = _allSdkService.GetCardsByCHID(query);
            return result;
        }
        [HttpPost("DeletePhoto")]
        public DeletePhotoQuery DeletePhoto([FromBody]DeletePhotoQuery query)
        {
            var result = _allSdkService.DeletePhoto(query);
            return result;
        }
        [HttpPost("DeleteSignature")]
        public DeleteSignatureQuery DeleteSignature([FromBody]DeleteSignatureQuery query)
        {
            var result = _allSdkService.DeleteSignature(query);
            return result;
        }
        [HttpPost("GetCardsWithoutCHIDByAcctID")]
        public GetCardsWithoutCHIDByAcctIDQuery GetCardsWithoutCHIDByAcctID([FromBody]GetCardsWithoutCHIDByAcctIDQuery query)
        {
            var result = _allSdkService.GetCardsWithoutCHIDByAcctID(query);
            return result;
        }
        [HttpPost("GetAccountByAcctID")]
        public GetAccountByAcctIDQuery GetAccountByAcctID([FromBody]GetAccountByAcctIDQuery query)
        {
            var result = _allSdkService.GetAccountByAcctID(query);
            return result;
        }
        [HttpPost("GetPanelsByAcctID")]
        public GetPanelsByAcctIDQuery GetPanelsByAcctID([FromBody]GetPanelsByAcctIDQuery query)
        {
            var result = _allSdkService.GetPanelsByAcctID(query);
            return result;
        }
        [HttpPost("GetOutputsByPanelID")]
        public GetOutputsByPanelIDQuery GetOutputsByPanelID([FromBody]GetOutputsByPanelIDQuery query)
        {
            var result = _allSdkService.GetOutputsByPanelID(query);
            return result;
        }
        [HttpPost("GetGroupsByPanelID")]
        public GetGroupsByPanelIDQuery GetGroupsByPanelID([FromBody]GetGroupsByPanelIDQuery query)
        {
            var result = _allSdkService.GetGroupsByPanelID(query);
            return result;
        }
        [HttpPost("GetAvailableTimezonesOfPanel")]
        public GetAvailableTimezonesOfPanelQuery GetAvailableTimezonesOfPanel([FromBody]GetAvailableTimezonesOfPanelQuery query)
        {
            var result = _allSdkService.GetAvailableTimezonesOfPanel(query);
            return result;
        }
        [HttpPost("ConfigureOutputTimezone")]
        public ConfigureOutputTimezoneQuery ConfigureOutputTimezone([FromBody]ConfigureOutputTimezoneQuery query)
        {
            var result = _allSdkService.ConfigureOutputTimezone(query);
            return result;
        }
        [HttpPost("GetAssociatedTimezoneOfOutput")]
        public GetAssociatedTimezoneOfOutputQuery GetAssociatedTimezoneOfOutput([FromBody]GetAssociatedTimezoneOfOutputQuery query)
        {
            var result = _allSdkService.GetAssociatedTimezoneOfOutput(query);
            return result;
        }
        [HttpPost("ConfigureGroupTimezone")]
        public ConfigureGroupTimezoneQuery ConfigureGroupTimezone([FromBody]ConfigureGroupTimezoneQuery query)
        {
            var result = _allSdkService.ConfigureGroupTimezone(query);
            return result;
        }
        [HttpPost("GetAssociatedTimezoneOfGroup")]
        public GetAssociatedTimezoneOfGroupQuery GetAssociatedTimezoneOfGroup([FromBody]GetAssociatedTimezoneOfGroupQuery query)
        {
            var result = _allSdkService.GetAssociatedTimezoneOfGroup(query);
            return result;
        }
        [HttpPost("GetDeviceNameByHWDeviceID")]
        public GetDeviceNameByHWDeviceIDQuery GetDeviceNameByHWDeviceID([FromBody]GetDeviceNameByHWDeviceIDQuery query)
        {
            var result = _allSdkService.GetDeviceNameByHWDeviceID(query);
            return result;
        }
        [HttpPost("GetDevNameByDeviceID")]
        public GetDevNameByDeviceIDQuery GetDevNameByDeviceID([FromBody]GetDevNameByDeviceIDQuery query)
        {
            var result = _allSdkService.GetDevNameByDeviceID(query);
            return result;
        }
        [HttpPost("GetTimezoneNameByID")]
        public GetTimezoneNameByIDQuery GetTimezoneNameByID([FromBody]GetTimezoneNameByIDQuery query)
        {
            var result = _allSdkService.GetTimezoneNameByID(query);
            return result;
        }
        [HttpPost("AddTimezone")]
        public AddTimezoneQuery AddTimezone([FromBody]AddTimezoneQuery query)
        {
            var result = _allSdkService.AddTimezone(query);
            return result;
        }
        [HttpPost("EditTimeZone")]
        public EditTimeZoneQuery EditTimeZone([FromBody]EditTimeZoneQuery query)
        {
            var result = _allSdkService.EditTimeZone(query);
            return result;
        }
        [HttpPost("DeleteTimeZone")]
        public DeleteTimeZoneQuery DeleteTimeZone([FromBody]DeleteTimeZoneQuery query)
        {
            var result = _allSdkService.DeleteTimeZone(query);
            return result;
        }
        [HttpPost("GetTimeZoneByName")]
        public GetTimeZoneByNameQuery GetTimeZoneByName([FromBody]GetTimeZoneByNameQuery query)
        {
            var result = _allSdkService.GetTimeZoneByName(query);
            return result;
        }
        [HttpPost("ConfigureTimeZoneRange")]
        public ConfigureTimeZoneRangeQuery ConfigureTimeZoneRange([FromBody]ConfigureTimeZoneRangeQuery query)
        {
            var result = _allSdkService.ConfigureTimeZoneRange(query);
            return result;
        }
        [HttpPost("GetTimeZoneRangesByTZID")]
        public GetTimeZoneRangesByTZIDQuery GetTimeZoneRangesByTZID([FromBody]GetTimeZoneRangesByTZIDQuery query)
        {
            var result = _allSdkService.GetTimeZoneRangesByTZID(query);
            return result;
        }
        [HttpPost("DeleteTimeZoneRange")]
        public DeleteTimeZoneRangeQuery DeleteTimeZoneRange([FromBody]DeleteTimeZoneRangeQuery query)
        {
            var result = _allSdkService.DeleteTimeZoneRange(query);
            return result;
        }
        [HttpPost("ConfigurePanelTimeZone")]
        public ConfigurePanelTimeZoneQuery ConfigurePanelTimeZone([FromBody]ConfigurePanelTimeZoneQuery query)
        {
            var result = _allSdkService.ConfigurePanelTimeZone(query);
            return result;
        }
        [HttpPost("IsolateOperatorsForTZReassign")]
        public IsolateOperatorsForTZReassignQuery IsolateOperatorsForTZReassign([FromBody]IsolateOperatorsForTZReassignQuery query)
        {
            var result = _allSdkService.IsolateOperatorsForTZReassign(query);
            return result;
        }
        [HttpPost("IsolatePanelsForTZDelete")]
        public IsolatePanelsForTZDeleteQuery IsolatePanelsForTZDelete([FromBody]IsolatePanelsForTZDeleteQuery query)
        {
            var result = _allSdkService.IsolatePanelsForTZDelete(query);
            return result;
        }
        [HttpPost("IsolateAccessLevelsForTZReassign")]
        public IsolateAccessLevelsForTZReassignQuery IsolateAccessLevelsForTZReassign([FromBody]IsolateAccessLevelsForTZReassignQuery query)
        {
            var result = _allSdkService.IsolateAccessLevelsForTZReassign(query);
            return result;
        }
        [HttpPost("IsolateActionGroupsForTZReassign")]
        public IsolateActionGroupsForTZReassignQuery IsolateActionGroupsForTZReassign([FromBody]IsolateActionGroupsForTZReassignQuery query)
        {
            var result = _allSdkService.IsolateActionGroupsForTZReassign(query);
            return result;
        }
        [HttpPost("IsolateCardsForTZReassign")]
        public IsolateCardsForTZReassignQuery IsolateCardsForTZReassign([FromBody]IsolateCardsForTZReassignQuery query)
        {
            var result = _allSdkService.IsolateCardsForTZReassign(query);
            return result;
        }
        [HttpPost("IsolateADVsForTZReassign")]
        public IsolateADVsForTZReassignQuery IsolateADVsForTZReassign([FromBody]IsolateADVsForTZReassignQuery query)
        {
            var result = _allSdkService.IsolateADVsForTZReassign(query);
            return result;
        }
        [HttpPost("GetTZsForOperatorReassign")]
        public GetTZsForOperatorReassignQuery GetTZsForOperatorReassign([FromBody]GetTZsForOperatorReassignQuery query)
        {
            var result = _allSdkService.GetTZsForOperatorReassign(query);
            return result;
        }
        [HttpPost("GetTZsForReassign")]
        public GetTZsForReassignQuery GetTZsForReassign([FromBody]GetTZsForReassignQuery query)
        {
            var result = _allSdkService.GetTZsForReassign(query);
            return result;
        }
        [HttpPost("DeletePanelTZ")]
        public DeletePanelTZQuery DeletePanelTZ([FromBody]DeletePanelTZQuery query)
        {
            var result = _allSdkService.DeletePanelTZ(query);
            return result;
        }
        [HttpPost("ReassignOperatorTZ")]
        public ReassignOperatorTZQuery ReassignOperatorTZ([FromBody]ReassignOperatorTZQuery query)
        {
            var result = _allSdkService.ReassignOperatorTZ(query);
            return result;
        }
        [HttpPost("ReassignAccessLevelTZ")]
        public ReassignAccessLevelTZQuery ReassignAccessLevelTZ([FromBody]ReassignAccessLevelTZQuery query)
        {
            var result = _allSdkService.ReassignAccessLevelTZ(query);
            return result;
        }
        [HttpPost("ReassignActionGroupTZ")]
        public ReassignActionGroupTZQuery ReassignActionGroupTZ([FromBody]ReassignActionGroupTZQuery query)
        {
            var result = _allSdkService.ReassignActionGroupTZ(query);
            return result;
        }
        [HttpPost("ReassignCardTZ")]
        public ReassignCardTZQuery ReassignCardTZ([FromBody]ReassignCardTZQuery query)
        {
            var result = _allSdkService.ReassignCardTZ(query);
            return result;
        }
        [HttpPost("ReassignADVTZ")]
        public ReassignADVTZQuery ReassignADVTZ([FromBody]ReassignADVTZQuery query)
        {
            var result = _allSdkService.ReassignADVTZ(query);
            return result;
        }
        [HttpPost("AddHoliday")]
        public AddHolidayQuery AddHoliday([FromBody]AddHolidayQuery query)
        {
            var result = _allSdkService.AddHoliday(query);
            return result;
        }
        [HttpPost("EditHoliday")]
        public EditHolidayQuery EditHoliday([FromBody]EditHolidayQuery query)
        {
            var result = _allSdkService.EditHoliday(query);
            return result;
        }
        [HttpPost("DeleteHoliday")]
        public DeleteHolidayQuery DeleteHoliday([FromBody]DeleteHolidayQuery query)
        {
            var result = _allSdkService.DeleteHoliday(query);
            return result;
        }
        [HttpPost("AddHolidayGroup")]
        public AddHolidayGroupQuery AddHolidayGroup([FromBody]AddHolidayGroupQuery query)
        {
            var result = _allSdkService.AddHolidayGroup(query);
            return result;
        }
        [HttpPost("EditHolidayGroup")]
        public EditHolidayGroupQuery EditHolidayGroup([FromBody]EditHolidayGroupQuery query)
        {
            var result = _allSdkService.EditHolidayGroup(query);
            return result;
        }
        [HttpPost("DeleteHolidayGroup")]
        public DeleteHolidayGroupQuery DeleteHolidayGroup([FromBody]DeleteHolidayGroupQuery query)
        {
            var result = _allSdkService.DeleteHolidayGroup(query);
            return result;
        }
        [HttpPost("GetHolidayGroupsByAcctID")]
        public GetHolidayGroupsByAcctIDQuery GetHolidayGroupsByAcctID([FromBody]GetHolidayGroupsByAcctIDQuery query)
        {
            var result = _allSdkService.GetHolidayGroupsByAcctID(query);
            return result;
        }
        [HttpPost("GetHolidaysByHolidayGroupID")]
        public GetHolidaysByHolidayGroupIDQuery GetHolidaysByHolidayGroupID([FromBody]GetHolidaysByHolidayGroupIDQuery query)
        {
            var result = _allSdkService.GetHolidaysByHolidayGroupID(query);
            return result;
        }
        [HttpPost("GetHolidayByID")]
        public GetHolidayByIDQuery GetHolidayByID([FromBody]GetHolidayByIDQuery query)
        {
            var result = _allSdkService.GetHolidayByID(query);
            return result;
        }
        [HttpPost("ConfigurePanelHolidayGroup")]
        public ConfigurePanelHolidayGroupQuery ConfigurePanelHolidayGroup([FromBody]ConfigurePanelHolidayGroupQuery query)
        {
            var result = _allSdkService.ConfigurePanelHolidayGroup(query);
            return result;
        }
        [HttpPost("GetConfiguredHolidayGroupsByPanel")]
        public GetConfiguredHolidayGroupsByPanelQuery GetConfiguredHolidayGroupsByPanel([FromBody]GetConfiguredHolidayGroupsByPanelQuery query)
        {
            var result = _allSdkService.GetConfiguredHolidayGroupsByPanel(query);
            return result;
        }
        [HttpPost("GetDirectPointTZDetailsofReader")]
        public GetDirectPointTZDetailsofReaderQuery GetDirectPointTZDetailsofReader([FromBody]GetDirectPointTZDetailsofReaderQuery query)
        {
            var result = _allSdkService.GetDirectPointTZDetailsofReader(query);
            return result;
        }
        [HttpPost("ConnectWPDatabase")]
        public ConnectWPDatabaseQuery ConnectWPDatabase([FromBody]ConnectWPDatabaseQuery query)
        {
            var result = _allSdkService.ConnectWPDatabase(query);
            return result;
        }
        [HttpPost("DisconnectWPDatabase")]
        public DisconnectWPDatabaseQuery DisconnectWPDatabase([FromBody]DisconnectWPDatabaseQuery query)
        {
            var result = _allSdkService.DisconnectWPDatabase(query);
            return result;
        }
        [HttpPost("Disconnect")]
        public DisconnectQuery Disconnect([FromBody]DisconnectQuery query)
        {
            var result = _allSdkService.Disconnect(query);
            return result;
        }
        [HttpPost("BulkDeleteCards")]
        public BulkDeleteCardsQuery BulkDeleteCards([FromBody]BulkDeleteCardsQuery query)
        {
            var result = _allSdkService.BulkDeleteCards(query);
            return result;
        }
        [HttpPost("BulkAddCards")]
        public BulkAddCardsQuery BulkAddCards([FromBody]BulkAddCardsQuery query)
        {
            var result = _allSdkService.BulkAddCards(query);
            return result;
        }
        [HttpPost("AddUpdateCard")]
        public AddUpdateCardQuery AddUpdateCard([FromBody]AddUpdateCardQuery query)
        {
            var result = _allSdkService.AddUpdateCard(query);
            return result;
        }
        [HttpPost("AddUpdateAL")]
        public AddUpdateALQuery AddUpdateAL([FromBody]AddUpdateALQuery query)
        {
            var result = _allSdkService.AddUpdateAL(query);
            return result;
        }
        [HttpPost("GetPhotoSize")]
        public GetPhotoSizeQuery GetPhotoSize([FromBody]GetPhotoSizeQuery query)
        {
            var result = _allSdkService.GetPhotoSize(query);
            return result;
        }
        [HttpPost("GetPhoto")]
        public GetPhotoQuery GetPhoto([FromBody]GetPhotoQuery query)
        {
            var result = _allSdkService.GetPhoto(query);
            return result;
        }
        [HttpPost("GetSigSize")]
        public GetSigSizeQuery GetSigSize([FromBody]GetSigSizeQuery query)
        {
            var result = _allSdkService.GetSigSize(query);
            return result;
        }
        [HttpPost("GetSig")]
        public GetSigQuery GetSig([FromBody]GetSigQuery query)
        {
            var result = _allSdkService.GetSig(query);
            return result;
        }
        [HttpPost("DeleteSig")]
        public DeleteSigQuery DeleteSig([FromBody]DeleteSigQuery query)
        {
            var result = _allSdkService.DeleteSig(query);
            return result;
        }
        [HttpPost("ImportPhoto")]
        public ImportPhotoQuery ImportPhoto([FromBody]ImportPhotoQuery query)
        {
            var result = _allSdkService.ImportPhoto(query);
            return result;
        }
        [HttpPost("ImportSig")]
        public ImportSigQuery ImportSig([FromBody]ImportSigQuery query)
        {
            var result = _allSdkService.ImportSig(query);
            return result;
        }
        [HttpPost("IsConnected")]
        public IsConnectedQuery IsConnected([FromBody]IsConnectedQuery query)
        {
            var result = _allSdkService.IsConnected(query);
            return result;
        }
        [HttpPost("IsGroupChecked")]
        public IsGroupCheckedQuery IsGroupChecked([FromBody]IsGroupCheckedQuery query)
        {
            var result = _allSdkService.IsGroupChecked(query);
            return result;
        }
        [HttpPost("GetWPDBServerTZoffset")]
        public GetWPDBServerTZoffsetQuery GetWPDBServerTZoffset([FromBody]GetWPDBServerTZoffsetQuery query)
        {
            var result = _allSdkService.GetWPDBServerTZoffset(query);
            return result;
        }
        [HttpPost("GetReaderTZDetailsByAccountId")]
        public GetReaderTZDetailsByAccountIdQuery GetReaderTZDetailsByAccountId([FromBody]GetReaderTZDetailsByAccountIdQuery query)
        {
            var result = _allSdkService.GetReaderTZDetailsByAccountId(query);
            return result;
        }
        [HttpPost("GetWPDBServerTZ")]
        public GetWPDBServerTZQuery GetWPDBServerTZ([FromBody]GetWPDBServerTZQuery query)
        {
            var result = _allSdkService.GetWPDBServerTZ(query);
            return result;
        }
        [HttpPost("GetAvailableGroupsofReader")]
        public GetAvailableGroupsofReaderQuery GetAvailableGroupsofReader([FromBody]GetAvailableGroupsofReaderQuery query)
        {
            var result = _allSdkService.GetAvailableGroupsofReader(query);
            return result;
        }
        [HttpPost("GetAssociatedGroupofReader")]
        public GetAssociatedGroupofReaderQuery GetAssociatedGroupofReader([FromBody]GetAssociatedGroupofReaderQuery query)
        {
            var result = _allSdkService.GetAssociatedGroupofReader(query);
            return result;
        }
        [HttpPost("ConfigureEntranceAccess")]
        public ConfigureEntranceAccessQuery ConfigureEntranceAccess([FromBody]ConfigureEntranceAccessQuery query)
        {
            var result = _allSdkService.ConfigureEntranceAccess(query);
            return result;
        }
        [HttpPost("GetAvailableTimeZonesOfAccessReader")]
        public GetAvailableTimeZonesOfAccessReaderQuery GetAvailableTimeZonesOfAccessReader([FromBody]GetAvailableTimeZonesOfAccessReaderQuery query)
        {
            var result = _allSdkService.GetAvailableTimeZonesOfAccessReader(query);
            return result;
        }
        [HttpPost("GetCurrentOperator")]
        public GetCurrentOperatorQuery GetCurrentOperator([FromBody]GetCurrentOperatorQuery query)
        {
            var result = _allSdkService.GetCurrentOperator(query);
            return result;
        }
        [HttpPost("GetConfiguredTimezonesByPanel")]
        public GetConfiguredTimezonesByPanelQuery GetConfiguredTimezonesByPanel([FromBody]GetConfiguredTimezonesByPanelQuery query)
        {
            var result = _allSdkService.GetConfiguredTimezonesByPanel(query);
            return result;
        }
        [HttpPost("GetAllAccessLevels")]
        public GetAllAccessLevelsQuery GetAllAccessLevels([FromBody]GetAllAccessLevelsQuery query)
        {
            var result = _allSdkService.GetAllAccessLevels(query);
            return result;
        }
        [HttpPost("GetAllTimezones")]
        public GetAllTimezonesQuery GetAllTimezones([FromBody]GetAllTimezonesQuery query)
        {
            var result = _allSdkService.GetAllTimezones(query);
            return result;
        }
        [HttpPost("CreateAccessLevel")]
        public CreateAccessLevelQuery CreateAccessLevel([FromBody]CreateAccessLevelQuery query)
        {
            var result = _allSdkService.CreateAccessLevel(query);
            return result;
        }
        [HttpPost("CreateTimezone")]
        public CreateTimezoneQuery CreateTimezone([FromBody]CreateTimezoneQuery query)
        {
            var result = _allSdkService.CreateTimezone(query);
            return result;
        }
        [HttpPost("IsolatePanelsForHGDelete")]
        public IsolatePanelsForHGDeleteQuery IsolatePanelsForHGDelete([FromBody]IsolatePanelsForHGDeleteQuery query)
        {
            var result = _allSdkService.IsolatePanelsForHGDelete(query);
            return result;
        }
        [HttpPost("GetAccessTreeByName")]
        public GetAccessTreeByNameQuery GetAccessTreeByName([FromBody]GetAccessTreeByNameQuery query)
        {
            var result = _allSdkService.GetAccessTreeByName(query);
            return result;
        }
        [HttpPost("GetCardHolderSearchFieldsByAccountName")]
        public GetCardHolderSearchFieldsByAccountNameQuery GetCardHolderSearchFieldsByAccountName([FromBody]GetCardHolderSearchFieldsByAccountNameQuery query)
        {
            var result = _allSdkService.GetCardHolderSearchFieldsByAccountName(query);
            return result;
        }
        [HttpPost("GetCardHoldersOnSearch")]
        public GetCardHoldersOnSearchQuery GetCardHoldersOnSearch([FromBody]GetCardHoldersOnSearchQuery query)
        {
            var result = _allSdkService.GetCardHoldersOnSearch(query);
            return result;
        }
        [HttpPost("GetConfiguredWPDomains")]
        public GetConfiguredWPDomainsQuery GetConfiguredWPDomains([FromBody]GetConfiguredWPDomainsQuery query)
        {
            var result = _allSdkService.GetConfiguredWPDomains(query);
            return result;
        }
        [HttpPost("ConfigureOutputTimezoneEx")]
        public ConfigureOutputTimezoneExQuery ConfigureOutputTimezoneEx([FromBody]ConfigureOutputTimezoneExQuery query)
        {
            var result = _allSdkService.ConfigureOutputTimezoneEx(query);
            return result;
        }
        [HttpPost("GetAssociatedTimezoneOfOutputEx")]
        public GetAssociatedTimezoneOfOutputExQuery GetAssociatedTimezoneOfOutputEx([FromBody]GetAssociatedTimezoneOfOutputExQuery query)
        {
            var result = _allSdkService.GetAssociatedTimezoneOfOutputEx(query);
            return result;
        }

    }
}



//Login
//GetAccounts
//GetCardsByAccountName
//GetAccessLevelsByAccountName
//GetMaxCardNumberLength
//GetAccessLevelType
//GetCardNumeric
//AddCard
//DeleteCard
//GetCardByCardNumber
//EditCard
//AddAccessLevel
//EditAccessLevel
//DeleteAccessLevel
//DeleteAL
//IsolateAccessLevel
//GetAccessLevelForReassign
//ReassignAccessLevel
//ConfigureAccessLevel
//GetTimeZonesByAccountName
//GetWPDSN
//GetReadersByAccountName
//GetADVDetailsByAccountName
//GetAcctIDByHID
//GetAvailableTimeZonesOfReader
//GetAssociatedTimeZoneOfReader
//GetAccessAreaBranchesByAccountName
//GetReadersInAccessAreaBranch
//GetAvailableTimezonesOfBranch
//GetCardHoldersByAccountName
//GetNoteFieldTemplateDetailsByAccount
//AddCardHolder
//EditCardHolder
//DeleteCardHolder
//Logout
//GetAccessLevelByName
//GetCardHolderByCardHolderID
//GetAccountNameByAcctID
//GetAccessLevelNameByID
//GetCardsByCHID
//DeletePhoto
//DeleteSignature
//GetCardsWithoutCHIDByAcctID
//GetAccountByAcctID
//GetPanelsByAcctID
//GetOutputsByPanelID
//GetGroupsByPanelID
//GetAvailableTimezonesOfPanel
//ConfigureOutputTimezone
//GetAssociatedTimezoneOfOutput
//ConfigureGroupTimezone
//GetAssociatedTimezoneOfGroup
//GetDeviceNameByHWDeviceID
//GetDevNameByDeviceID
//GetTimezoneNameByID
//AddTimezone
//EditTimeZone
//DeleteTimeZone
//GetTimeZoneByName
//ConfigureTimeZoneRange
//GetTimeZoneRangesByTZID
//DeleteTimeZoneRange
//ConfigurePanelTimeZone
//IsolateOperatorsForTZReassign
//IsolatePanelsForTZDelete
//IsolateAccessLevelsForTZReassign
//IsolateActionGroupsForTZReassign
//IsolateCardsForTZReassign
//IsolateADVsForTZReassign
//GetTZsForOperatorReassign
//GetTZsForReassign
//DeletePanelTZ
//ReassignOperatorTZ
//ReassignAccessLevelTZ
//ReassignActionGroupTZ
//ReassignCardTZ
//ReassignADVTZ
//AddHoliday
//EditHoliday
//DeleteHoliday
//AddHolidayGroup
//EditHolidayGroup
//DeleteHolidayGroup
//GetHolidayGroupsByAcctID
//GetHolidaysByHolidayGroupID
//GetHolidayByID
//ConfigurePanelHolidayGroup
//GetConfiguredHolidayGroupsByPanel
//GetDirectPointTZDetailsofReader
//ConnectWPDatabase
//DisconnectWPDatabase
//Disconnect
//BulkDeleteCards
//BulkAddCards
//AddUpdateCard
//AddUpdateAL
//GetPhotoSize
//GetPhoto
//GetSigSize
//GetSig
//DeleteSig
//ImportPhoto
//ImportSig
//IsConnected
//IsGroupChecked
//GetWPDBServerTZoffset
//GetReaderTZDetailsByAccountId
//GetWPDBServerTZ
//GetAvailableGroupsofReader
//GetAssociatedGroupofReader
//ConfigureEntranceAccess
//GetAvailableTimeZonesOfAccessReader
//ConfigureEntranceAccess
//GetConfiguredTimezonesByPanel
//GetAllAccessLevels
//GetAllTimezones
//CreateAccessLevel
//CreateTimezone
//IsolatePanelsForHGDelete
//GetAccessTreeByName
//GetCardHolderSearchFieldsByAccountName
//GetCardHoldersOnSearch
//GetConfiguredWPDomains
//ConfigureOutputTimezoneEx
//GetAssociatedTimezoneOfOutputEx

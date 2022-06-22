using KT.Common.WebApi.HttpApi;
using KT.Common.Core.Utils;
using KT.WinPak.Data.Models;
using KT.WinPak.SDK;
using KT.WinPak.SDK.IServices;
using KT.WinPak.SDK.Models;
using KT.WinPak.SDK.Queries;
using KT.WinPak.SDK.Services;
using KT.WinPak.Service.IServices;
using KT.WinPak.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace KT.WinPak.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllQueryController : ControllerBase
    {
        private ILogger<AllQueryController> _logger;
        private IAllSdkService _allSdkService;

        public AllQueryController(ILogger<AllQueryController> logger, IAllSdkService allSdkService)
        {
            _logger = logger;
            _allSdkService = allSdkService;
        }
        //GetCurrentOperator

        [HttpGet("Login")]
        public LoginQuery Login()
        {
            return new LoginQuery();
        }
        [HttpGet("GetAccounts")]
        public GetAccountsQuery GetAccounts()
        {
            return new GetAccountsQuery();
        }
        [HttpGet("GetCardsByAccountName")]
        public GetCardsByAccountNameQuery GetCardsByAccountName()
        {
            return new GetCardsByAccountNameQuery();
        }
        [HttpGet("GetAccessLevelsByAccountName")]
        public GetAccessLevelsByAccountNameQuery GetAccessLevelsByAccountName()
        {
            return new GetAccessLevelsByAccountNameQuery();
        }
        [HttpGet("GetMaxCardNumberLength")]
        public GetMaxCardNumberLengthQuery GetMaxCardNumberLength()
        {
            return new GetMaxCardNumberLengthQuery();
        }
        [HttpGet("GetAccessLevelType")]
        public GetAccessLevelTypeQuery GetAccessLevelType()
        {
            return new GetAccessLevelTypeQuery();
        }
        [HttpGet("GetCardNumeric")]
        public GetCardNumericQuery GetCardNumeric()
        {
            return new GetCardNumericQuery();
        }
        [HttpGet("AddCard")]
        public AddCardQuery AddCard()
        {
            return new AddCardQuery();
        }
        [HttpGet("DeleteCard")]
        public DeleteCardQuery DeleteCard()
        {
            return new DeleteCardQuery();
        }
        [HttpGet("GetCardByCardNumber")]
        public GetCardByCardNumberQuery GetCardByCardNumber()
        {
            return new GetCardByCardNumberQuery();
        }
        [HttpGet("EditCard")]
        public EditCardQuery EditCard()
        {
            return new EditCardQuery();
        }
        [HttpGet("AddAccessLevel")]
        public AddAccessLevelQuery AddAccessLevel()
        {
            return new AddAccessLevelQuery();
        }
        [HttpGet("EditAccessLevel")]
        public EditAccessLevelQuery EditAccessLevel()
        {
            return new EditAccessLevelQuery();
        }
        [HttpGet("DeleteAccessLevel")]
        public DeleteAccessLevelQuery DeleteAccessLevel()
        {
            return new DeleteAccessLevelQuery();
        }
        [HttpGet("DeleteAL")]
        public DeleteALQuery DeleteAL()
        {
            return new DeleteALQuery();
        }
        [HttpGet("IsolateAccessLevel")]
        public IsolateAccessLevelQuery IsolateAccessLevel()
        {
            return new IsolateAccessLevelQuery();
        }
        [HttpGet("GetAccessLevelForReassign")]
        public GetAccessLevelForReassignQuery GetAccessLevelForReassign()
        {
            return new GetAccessLevelForReassignQuery();
        }
        [HttpGet("ReassignAccessLevel")]
        public ReassignAccessLevelQuery ReassignAccessLevel()
        {
            return new ReassignAccessLevelQuery();
        }
        [HttpGet("ConfigureAccessLevel")]
        public ConfigureAccessLevelQuery ConfigureAccessLevel()
        {
            return new ConfigureAccessLevelQuery();
        }
        [HttpGet("GetTimeZonesByAccountName")]
        public GetTimeZonesByAccountNameQuery GetTimeZonesByAccountName()
        {
            return new GetTimeZonesByAccountNameQuery();
        }
        [HttpGet("GetWPDSN")]
        public GetWPDSNQuery GetWPDSN()
        {
            return new GetWPDSNQuery();
        }
        [HttpGet("GetReadersByAccountName")]
        public GetReadersByAccountNameQuery GetReadersByAccountName()
        {
            return new GetReadersByAccountNameQuery();
        }
        [HttpGet("GetADVDetailsByAccountName")]
        public GetADVDetailsByAccountNameQuery GetADVDetailsByAccountName()
        {
            return new GetADVDetailsByAccountNameQuery();
        }
        [HttpGet("GetAcctIDByHID")]
        public GetAcctIDByHIDQuery GetAcctIDByHID()
        {
            return new GetAcctIDByHIDQuery();
        }
        [HttpGet("GetAvailableTimeZonesOfReader")]
        public GetAvailableTimeZonesOfReaderQuery GetAvailableTimeZonesOfReader()
        {
            return new GetAvailableTimeZonesOfReaderQuery();
        }
        [HttpGet("GetAssociatedTimeZoneOfReader")]
        public GetAssociatedTimeZoneOfReaderQuery GetAssociatedTimeZoneOfReader()
        {
            return new GetAssociatedTimeZoneOfReaderQuery();
        }
        [HttpGet("GetAccessAreaBranchesByAccountName")]
        public GetAccessAreaBranchesByAccountNameQuery GetAccessAreaBranchesByAccountName()
        {
            return new GetAccessAreaBranchesByAccountNameQuery();
        }
        [HttpGet("GetReadersInAccessAreaBranch")]
        public GetReadersInAccessAreaBranchQuery GetReadersInAccessAreaBranch()
        {
            return new GetReadersInAccessAreaBranchQuery();
        }
        [HttpGet("GetAvailableTimezonesOfBranch")]
        public GetAvailableTimezonesOfBranchQuery GetAvailableTimezonesOfBranch()
        {
            return new GetAvailableTimezonesOfBranchQuery();
        }
        [HttpGet("GetCardHoldersByAccountName")]
        public GetCardHoldersByAccountNameQuery GetCardHoldersByAccountName()
        {
            return new GetCardHoldersByAccountNameQuery();
        }
        [HttpGet("GetNoteFieldTemplateDetailsByAccount")]
        public GetNoteFieldTemplateDetailsByAccountQuery GetNoteFieldTemplateDetailsByAccount()
        {
            return new GetNoteFieldTemplateDetailsByAccountQuery();
        }
        [HttpGet("AddCardHolder")]
        public AddCardHolderQuery AddCardHolder()
        {
            return new AddCardHolderQuery();
        }
        [HttpGet("EditCardHolder")]
        public EditCardHolderQuery EditCardHolder()
        {
            return new EditCardHolderQuery();
        }
        [HttpGet("DeleteCardHolder")]
        public DeleteCardHolderQuery DeleteCardHolder()
        {
            return new DeleteCardHolderQuery();
        }
        [HttpGet("Logout")]
        public LogoutQuery Logout()
        {
            return new LogoutQuery();
        }
        [HttpGet("GetAccessLevelByName")]
        public GetAccessLevelByNameQuery GetAccessLevelByName()
        {
            return new GetAccessLevelByNameQuery();
        }
        [HttpGet("GetCardHolderByCardHolderID")]
        public GetCardHolderByCardHolderIDQuery GetCardHolderByCardHolderID()
        {
            return new GetCardHolderByCardHolderIDQuery();
        }
        [HttpGet("GetAccountNameByAcctID")]
        public GetAccountNameByAcctIDQuery GetAccountNameByAcctID()
        {
            return new GetAccountNameByAcctIDQuery();
        }
        [HttpGet("GetAccessLevelNameByID")]
        public GetAccessLevelNameByIDQuery GetAccessLevelNameByID()
        {
            return new GetAccessLevelNameByIDQuery();
        }
        [HttpGet("GetCardsByCHID")]
        public GetCardsByCHIDQuery GetCardsByCHID()
        {
            return new GetCardsByCHIDQuery();
        }
        [HttpGet("DeletePhoto")]
        public DeletePhotoQuery DeletePhoto()
        {
            return new DeletePhotoQuery();
        }
        [HttpGet("DeleteSignature")]
        public DeleteSignatureQuery DeleteSignature()
        {
            return new DeleteSignatureQuery();
        }
        [HttpGet("GetCardsWithoutCHIDByAcctID")]
        public GetCardsWithoutCHIDByAcctIDQuery GetCardsWithoutCHIDByAcctID()
        {
            return new GetCardsWithoutCHIDByAcctIDQuery();
        }
        [HttpGet("GetAccountByAcctID")]
        public GetAccountByAcctIDQuery GetAccountByAcctID()
        {
            return new GetAccountByAcctIDQuery();
        }
        [HttpGet("GetPanelsByAcctID")]
        public GetPanelsByAcctIDQuery GetPanelsByAcctID()
        {
            return new GetPanelsByAcctIDQuery();
        }
        [HttpGet("GetOutputsByPanelID")]
        public GetOutputsByPanelIDQuery GetOutputsByPanelID()
        {
            return new GetOutputsByPanelIDQuery();
        }
        [HttpGet("GetGroupsByPanelID")]
        public GetGroupsByPanelIDQuery GetGroupsByPanelID()
        {
            return new GetGroupsByPanelIDQuery();
        }
        [HttpGet("GetAvailableTimezonesOfPanel")]
        public GetAvailableTimezonesOfPanelQuery GetAvailableTimezonesOfPanel()
        {
            return new GetAvailableTimezonesOfPanelQuery();
        }
        [HttpGet("ConfigureOutputTimezone")]
        public ConfigureOutputTimezoneQuery ConfigureOutputTimezone()
        {
            return new ConfigureOutputTimezoneQuery();
        }
        [HttpGet("GetAssociatedTimezoneOfOutput")]
        public GetAssociatedTimezoneOfOutputQuery GetAssociatedTimezoneOfOutput()
        {
            return new GetAssociatedTimezoneOfOutputQuery();
        }
        [HttpGet("ConfigureGroupTimezone")]
        public ConfigureGroupTimezoneQuery ConfigureGroupTimezone()
        {
            return new ConfigureGroupTimezoneQuery();
        }
        [HttpGet("GetAssociatedTimezoneOfGroup")]
        public GetAssociatedTimezoneOfGroupQuery GetAssociatedTimezoneOfGroup()
        {
            return new GetAssociatedTimezoneOfGroupQuery();
        }
        [HttpGet("GetDeviceNameByHWDeviceID")]
        public GetDeviceNameByHWDeviceIDQuery GetDeviceNameByHWDeviceID()
        {
            return new GetDeviceNameByHWDeviceIDQuery();
        }
        [HttpGet("GetDevNameByDeviceID")]
        public GetDevNameByDeviceIDQuery GetDevNameByDeviceID()
        {
            return new GetDevNameByDeviceIDQuery();
        }
        [HttpGet("GetTimezoneNameByID")]
        public GetTimezoneNameByIDQuery GetTimezoneNameByID()
        {
            return new GetTimezoneNameByIDQuery();
        }
        [HttpGet("AddTimezone")]
        public AddTimezoneQuery AddTimezone()
        {
            return new AddTimezoneQuery();
        }
        [HttpGet("EditTimeZone")]
        public EditTimeZoneQuery EditTimeZone()
        {
            return new EditTimeZoneQuery();
        }
        [HttpGet("DeleteTimeZone")]
        public DeleteTimeZoneQuery DeleteTimeZone()
        {
            return new DeleteTimeZoneQuery();
        }
        [HttpGet("GetTimeZoneByName")]
        public GetTimeZoneByNameQuery GetTimeZoneByName()
        {
            return new GetTimeZoneByNameQuery();
        }
        [HttpGet("ConfigureTimeZoneRange")]
        public ConfigureTimeZoneRangeQuery ConfigureTimeZoneRange()
        {
            return new ConfigureTimeZoneRangeQuery();
        }
        [HttpGet("GetTimeZoneRangesByTZID")]
        public GetTimeZoneRangesByTZIDQuery GetTimeZoneRangesByTZID()
        {
            return new GetTimeZoneRangesByTZIDQuery();
        }
        [HttpGet("DeleteTimeZoneRange")]
        public DeleteTimeZoneRangeQuery DeleteTimeZoneRange()
        {
            return new DeleteTimeZoneRangeQuery();
        }
        [HttpGet("ConfigurePanelTimeZone")]
        public ConfigurePanelTimeZoneQuery ConfigurePanelTimeZone()
        {
            return new ConfigurePanelTimeZoneQuery();
        }
        [HttpGet("IsolateOperatorsForTZReassign")]
        public IsolateOperatorsForTZReassignQuery IsolateOperatorsForTZReassign()
        {
            return new IsolateOperatorsForTZReassignQuery();
        }
        [HttpGet("IsolatePanelsForTZDelete")]
        public IsolatePanelsForTZDeleteQuery IsolatePanelsForTZDelete()
        {
            return new IsolatePanelsForTZDeleteQuery();
        }
        [HttpGet("IsolateAccessLevelsForTZReassign")]
        public IsolateAccessLevelsForTZReassignQuery IsolateAccessLevelsForTZReassign()
        {
            return new IsolateAccessLevelsForTZReassignQuery();
        }
        [HttpGet("IsolateActionGroupsForTZReassign")]
        public IsolateActionGroupsForTZReassignQuery IsolateActionGroupsForTZReassign()
        {
            return new IsolateActionGroupsForTZReassignQuery();
        }
        [HttpGet("IsolateCardsForTZReassign")]
        public IsolateCardsForTZReassignQuery IsolateCardsForTZReassign()
        {
            return new IsolateCardsForTZReassignQuery();
        }
        [HttpGet("IsolateADVsForTZReassign")]
        public IsolateADVsForTZReassignQuery IsolateADVsForTZReassign()
        {
            return new IsolateADVsForTZReassignQuery();
        }
        [HttpGet("GetTZsForOperatorReassign")]
        public GetTZsForOperatorReassignQuery GetTZsForOperatorReassign()
        {
            return new GetTZsForOperatorReassignQuery();
        }
        [HttpGet("GetTZsForReassign")]
        public GetTZsForReassignQuery GetTZsForReassign()
        {
            return new GetTZsForReassignQuery();
        }
        [HttpGet("DeletePanelTZ")]
        public DeletePanelTZQuery DeletePanelTZ()
        {
            return new DeletePanelTZQuery();
        }
        [HttpGet("ReassignOperatorTZ")]
        public ReassignOperatorTZQuery ReassignOperatorTZ()
        {
            return new ReassignOperatorTZQuery();
        }
        [HttpGet("ReassignAccessLevelTZ")]
        public ReassignAccessLevelTZQuery ReassignAccessLevelTZ()
        {
            return new ReassignAccessLevelTZQuery();
        }
        [HttpGet("ReassignActionGroupTZ")]
        public ReassignActionGroupTZQuery ReassignActionGroupTZ()
        {
            return new ReassignActionGroupTZQuery();
        }
        [HttpGet("ReassignCardTZ")]
        public ReassignCardTZQuery ReassignCardTZ()
        {
            return new ReassignCardTZQuery();
        }
        [HttpGet("ReassignADVTZ")]
        public ReassignADVTZQuery ReassignADVTZ()
        {
            return new ReassignADVTZQuery();
        }
        [HttpGet("AddHoliday")]
        public AddHolidayQuery AddHoliday()
        {
            return new AddHolidayQuery();
        }
        [HttpGet("EditHoliday")]
        public EditHolidayQuery EditHoliday()
        {
            return new EditHolidayQuery();
        }
        [HttpGet("DeleteHoliday")]
        public DeleteHolidayQuery DeleteHoliday()
        {
            return new DeleteHolidayQuery();
        }
        [HttpGet("AddHolidayGroup")]
        public AddHolidayGroupQuery AddHolidayGroup()
        {
            return new AddHolidayGroupQuery();
        }
        [HttpGet("EditHolidayGroup")]
        public EditHolidayGroupQuery EditHolidayGroup()
        {
            return new EditHolidayGroupQuery();
        }
        [HttpGet("DeleteHolidayGroup")]
        public DeleteHolidayGroupQuery DeleteHolidayGroup()
        {
            return new DeleteHolidayGroupQuery();
        }
        [HttpGet("GetHolidayGroupsByAcctID")]
        public GetHolidayGroupsByAcctIDQuery GetHolidayGroupsByAcctID()
        {
            return new GetHolidayGroupsByAcctIDQuery();
        }
        [HttpGet("GetHolidaysByHolidayGroupID")]
        public GetHolidaysByHolidayGroupIDQuery GetHolidaysByHolidayGroupID()
        {
            return new GetHolidaysByHolidayGroupIDQuery();
        }
        [HttpGet("GetHolidayByID")]
        public GetHolidayByIDQuery GetHolidayByID()
        {
            return new GetHolidayByIDQuery();
        }
        [HttpGet("ConfigurePanelHolidayGroup")]
        public ConfigurePanelHolidayGroupQuery ConfigurePanelHolidayGroup()
        {
            return new ConfigurePanelHolidayGroupQuery();
        }
        [HttpGet("GetConfiguredHolidayGroupsByPanel")]
        public GetConfiguredHolidayGroupsByPanelQuery GetConfiguredHolidayGroupsByPanel()
        {
            return new GetConfiguredHolidayGroupsByPanelQuery();
        }
        [HttpGet("GetDirectPointTZDetailsofReader")]
        public GetDirectPointTZDetailsofReaderQuery GetDirectPointTZDetailsofReader()
        {
            return new GetDirectPointTZDetailsofReaderQuery();
        }
        [HttpGet("ConnectWPDatabase")]
        public ConnectWPDatabaseQuery ConnectWPDatabase()
        {
            return new ConnectWPDatabaseQuery();
        }
        [HttpGet("DisconnectWPDatabase")]
        public DisconnectWPDatabaseQuery DisconnectWPDatabase()
        {
            return new DisconnectWPDatabaseQuery();
        }
        [HttpGet("Disconnect")]
        public DisconnectQuery Disconnect()
        {
            return new DisconnectQuery();
        }
        [HttpGet("BulkDeleteCards")]
        public BulkDeleteCardsQuery BulkDeleteCards()
        {
            return new BulkDeleteCardsQuery();
        }
        [HttpGet("BulkAddCards")]
        public BulkAddCardsQuery BulkAddCards()
        {
            return new BulkAddCardsQuery();
        }
        [HttpGet("AddUpdateCard")]
        public AddUpdateCardQuery AddUpdateCard()
        {
            return new AddUpdateCardQuery();
        }
        [HttpGet("AddUpdateAL")]
        public AddUpdateALQuery AddUpdateAL()
        {
            return new AddUpdateALQuery();
        }
        [HttpGet("GetPhotoSize")]
        public GetPhotoSizeQuery GetPhotoSize()
        {
            return new GetPhotoSizeQuery();
        }
        [HttpGet("GetPhoto")]
        public GetPhotoQuery GetPhoto()
        {
            return new GetPhotoQuery();
        }
        [HttpGet("GetSigSize")]
        public GetSigSizeQuery GetSigSize()
        {
            return new GetSigSizeQuery();
        }
        [HttpGet("GetSig")]
        public GetSigQuery GetSig()
        {
            return new GetSigQuery();
        }
        [HttpGet("DeleteSig")]
        public DeleteSigQuery DeleteSig()
        {
            return new DeleteSigQuery();
        }
        [HttpGet("ImportPhoto")]
        public ImportPhotoQuery ImportPhoto()
        {
            return new ImportPhotoQuery();
        }
        [HttpGet("ImportSig")]
        public ImportSigQuery ImportSig()
        {
            return new ImportSigQuery();
        }
        [HttpGet("IsConnected")]
        public IsConnectedQuery IsConnected()
        {
            return new IsConnectedQuery();
        }
        [HttpGet("IsGroupChecked")]
        public IsGroupCheckedQuery IsGroupChecked()
        {
            return new IsGroupCheckedQuery();
        }
        [HttpGet("GetWPDBServerTZoffset")]
        public GetWPDBServerTZoffsetQuery GetWPDBServerTZoffset()
        {
            return new GetWPDBServerTZoffsetQuery();
        }
        [HttpGet("GetReaderTZDetailsByAccountId")]
        public GetReaderTZDetailsByAccountIdQuery GetReaderTZDetailsByAccountId()
        {
            return new GetReaderTZDetailsByAccountIdQuery();
        }
        [HttpGet("GetWPDBServerTZ")]
        public GetWPDBServerTZQuery GetWPDBServerTZ()
        {
            return new GetWPDBServerTZQuery();
        }
        [HttpGet("GetAvailableGroupsofReader")]
        public GetAvailableGroupsofReaderQuery GetAvailableGroupsofReader()
        {
            return new GetAvailableGroupsofReaderQuery();
        }
        [HttpGet("GetAssociatedGroupofReader")]
        public GetAssociatedGroupofReaderQuery GetAssociatedGroupofReader()
        {
            return new GetAssociatedGroupofReaderQuery();
        }
        [HttpGet("ConfigureEntranceAccess")]
        public ConfigureEntranceAccessQuery ConfigureEntranceAccess()
        {
            return new ConfigureEntranceAccessQuery();
        }
        [HttpGet("GetAvailableTimeZonesOfAccessReader")]
        public GetAvailableTimeZonesOfAccessReaderQuery GetAvailableTimeZonesOfAccessReader()
        {
            return new GetAvailableTimeZonesOfAccessReaderQuery();
        }
        [HttpGet("GetCurrentOperator")]
        public GetCurrentOperatorQuery GetCurrentOperator()
        {
            return new GetCurrentOperatorQuery();
        }
        [HttpGet("GetConfiguredTimezonesByPanel")]
        public GetConfiguredTimezonesByPanelQuery GetConfiguredTimezonesByPanel()
        {
            return new GetConfiguredTimezonesByPanelQuery();
        }
        [HttpGet("GetAllAccessLevels")]
        public GetAllAccessLevelsQuery GetAllAccessLevels()
        {
            return new GetAllAccessLevelsQuery();
        }
        [HttpGet("GetAllTimezones")]
        public GetAllTimezonesQuery GetAllTimezones()
        {
            return new GetAllTimezonesQuery();
        }
        [HttpGet("CreateAccessLevel")]
        public CreateAccessLevelQuery CreateAccessLevel()
        {
            return new CreateAccessLevelQuery();
        }
        [HttpGet("CreateTimezone")]
        public CreateTimezoneQuery CreateTimezone()
        {
            return new CreateTimezoneQuery();
        }
        [HttpGet("IsolatePanelsForHGDelete")]
        public IsolatePanelsForHGDeleteQuery IsolatePanelsForHGDelete()
        {
            return new IsolatePanelsForHGDeleteQuery();
        }
        [HttpGet("GetAccessTreeByName")]
        public GetAccessTreeByNameQuery GetAccessTreeByName()
        {
            return new GetAccessTreeByNameQuery();
        }
        [HttpGet("GetCardHolderSearchFieldsByAccountName")]
        public GetCardHolderSearchFieldsByAccountNameQuery GetCardHolderSearchFieldsByAccountName()
        {
            return new GetCardHolderSearchFieldsByAccountNameQuery();
        }
        [HttpGet("GetCardHoldersOnSearch")]
        public GetCardHoldersOnSearchQuery GetCardHoldersOnSearch()
        {
            return new GetCardHoldersOnSearchQuery();
        }
        [HttpGet("GetConfiguredWPDomains")]
        public GetConfiguredWPDomainsQuery GetConfiguredWPDomains()
        {
            return new GetConfiguredWPDomainsQuery();
        }
        [HttpGet("ConfigureOutputTimezoneEx")]
        public ConfigureOutputTimezoneExQuery ConfigureOutputTimezoneEx()
        {
            return new ConfigureOutputTimezoneExQuery();
        }
        [HttpGet("GetAssociatedTimezoneOfOutputEx")]
        public GetAssociatedTimezoneOfOutputExQuery GetAssociatedTimezoneOfOutputEx()
        {
            return new GetAssociatedTimezoneOfOutputExQuery();
        }
    }
}
﻿using KT.WinPak.SDK.V48.Models;
using KT.WinPak.SDK.V48.Queries;
using KT.WinPak.SDK.V48.Services;
using NCIHelperLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.WinPak.SDK.V48.IServices
{
    public interface IAllSdkService
    {
        WPAccountClass GetWPAccountClass();
        CardHolderClass GetCardHolderClass();
        AccessLevelClass GetAccessLevelClass();
        CardClass GetCardClass();
        HWDeviceClass GetHWDeviceClass();

        LoginQuery Login(LoginQuery query);
        GetAccountsQuery GetAccounts(GetAccountsQuery query);
        GetCardsByAccountNameQuery GetCardsByAccountName(GetCardsByAccountNameQuery query);
        GetAccessLevelsByAccountNameQuery GetAccessLevelsByAccountName(GetAccessLevelsByAccountNameQuery query);
        GetMaxCardNumberLengthQuery GetMaxCardNumberLength(GetMaxCardNumberLengthQuery query);
        GetAccessLevelTypeQuery GetAccessLevelType(GetAccessLevelTypeQuery query);
        GetCardNumericQuery GetCardNumeric(GetCardNumericQuery query);
        AddCardQuery AddCard(AddCardQuery query);
        DeleteCardQuery DeleteCard(DeleteCardQuery query);
        GetCardByCardNumberQuery GetCardByCardNumber(GetCardByCardNumberQuery query);
        EditCardQuery EditCard(EditCardQuery query);
        AddAccessLevelQuery AddAccessLevel(AddAccessLevelQuery query);
        EditAccessLevelQuery EditAccessLevel(EditAccessLevelQuery query);
        DeleteAccessLevelQuery DeleteAccessLevel(DeleteAccessLevelQuery query);
        DeleteALQuery DeleteAL(DeleteALQuery query);
        IsolateAccessLevelQuery IsolateAccessLevel(IsolateAccessLevelQuery query);
        GetAccessLevelForReassignQuery GetAccessLevelForReassign(GetAccessLevelForReassignQuery query);
        ReassignAccessLevelQuery ReassignAccessLevel(ReassignAccessLevelQuery query);
        ConfigureAccessLevelQuery ConfigureAccessLevel(ConfigureAccessLevelQuery query);
        GetTimeZonesByAccountNameQuery GetTimeZonesByAccountName(GetTimeZonesByAccountNameQuery query);
        GetWPDSNQuery GetWPDSN(GetWPDSNQuery query);
        GetReadersByAccountNameQuery GetReadersByAccountName(GetReadersByAccountNameQuery query);
        GetADVDetailsByAccountNameQuery GetADVDetailsByAccountName(GetADVDetailsByAccountNameQuery query);
        GetAcctIDByHIDQuery GetAcctIDByHID(GetAcctIDByHIDQuery query);
        GetAvailableTimeZonesOfReaderQuery GetAvailableTimeZonesOfReader(GetAvailableTimeZonesOfReaderQuery query);
        GetAssociatedTimeZoneOfReaderQuery GetAssociatedTimeZoneOfReader(GetAssociatedTimeZoneOfReaderQuery query);
        GetAccessAreaBranchesByAccountNameQuery GetAccessAreaBranchesByAccountName(GetAccessAreaBranchesByAccountNameQuery query);
        GetReadersInAccessAreaBranchQuery GetReadersInAccessAreaBranch(GetReadersInAccessAreaBranchQuery query);
        GetAvailableTimezonesOfBranchQuery GetAvailableTimezonesOfBranch(GetAvailableTimezonesOfBranchQuery query);
        GetCardHoldersByAccountNameQuery GetCardHoldersByAccountName(GetCardHoldersByAccountNameQuery query);
        GetNoteFieldTemplateDetailsByAccountQuery GetNoteFieldTemplateDetailsByAccount(GetNoteFieldTemplateDetailsByAccountQuery query);
        AddCardHolderQuery AddCardHolder(AddCardHolderQuery query);
        EditCardHolderQuery EditCardHolder(EditCardHolderQuery query);
        DeleteCardHolderQuery DeleteCardHolder(DeleteCardHolderQuery query);
        LogoutQuery Logout(LogoutQuery query);
        GetAccessLevelByNameQuery GetAccessLevelByName(GetAccessLevelByNameQuery query);
        GetCardHolderByCardHolderIDQuery GetCardHolderByCardHolderID(GetCardHolderByCardHolderIDQuery query);
        GetAccountNameByAcctIDQuery GetAccountNameByAcctID(GetAccountNameByAcctIDQuery query);
        GetAccessLevelNameByIDQuery GetAccessLevelNameByID(GetAccessLevelNameByIDQuery query);
        GetCardsByCHIDQuery GetCardsByCHID(GetCardsByCHIDQuery query);
        DeletePhotoQuery DeletePhoto(DeletePhotoQuery query);
        DeleteSignatureQuery DeleteSignature(DeleteSignatureQuery query);
        GetCardsWithoutCHIDByAcctIDQuery GetCardsWithoutCHIDByAcctID(GetCardsWithoutCHIDByAcctIDQuery query);
        GetAccountByAcctIDQuery GetAccountByAcctID(GetAccountByAcctIDQuery query);
        GetPanelsByAcctIDQuery GetPanelsByAcctID(GetPanelsByAcctIDQuery query);
        GetOutputsByPanelIDQuery GetOutputsByPanelID(GetOutputsByPanelIDQuery query);
        GetGroupsByPanelIDQuery GetGroupsByPanelID(GetGroupsByPanelIDQuery query);
        GetAvailableTimezonesOfPanelQuery GetAvailableTimezonesOfPanel(GetAvailableTimezonesOfPanelQuery query);
        ConfigureOutputTimezoneQuery ConfigureOutputTimezone(ConfigureOutputTimezoneQuery query);
        GetAssociatedTimezoneOfOutputQuery GetAssociatedTimezoneOfOutput(GetAssociatedTimezoneOfOutputQuery query);
        ConfigureGroupTimezoneQuery ConfigureGroupTimezone(ConfigureGroupTimezoneQuery query);
        GetAssociatedTimezoneOfGroupQuery GetAssociatedTimezoneOfGroup(GetAssociatedTimezoneOfGroupQuery query);
        GetDeviceNameByHWDeviceIDQuery GetDeviceNameByHWDeviceID(GetDeviceNameByHWDeviceIDQuery query);
        GetDevNameByDeviceIDQuery GetDevNameByDeviceID(GetDevNameByDeviceIDQuery query);
        GetTimezoneNameByIDQuery GetTimezoneNameByID(GetTimezoneNameByIDQuery query);
        AddTimezoneQuery AddTimezone(AddTimezoneQuery query);
        EditTimeZoneQuery EditTimeZone(EditTimeZoneQuery query);
        DeleteTimeZoneQuery DeleteTimeZone(DeleteTimeZoneQuery query);
        GetTimeZoneByNameQuery GetTimeZoneByName(GetTimeZoneByNameQuery query);
        ConfigureTimeZoneRangeQuery ConfigureTimeZoneRange(ConfigureTimeZoneRangeQuery query);
        GetTimeZoneRangesByTZIDQuery GetTimeZoneRangesByTZID(GetTimeZoneRangesByTZIDQuery query);
        DeleteTimeZoneRangeQuery DeleteTimeZoneRange(DeleteTimeZoneRangeQuery query);
        ConfigurePanelTimeZoneQuery ConfigurePanelTimeZone(ConfigurePanelTimeZoneQuery query);
        IsolateOperatorsForTZReassignQuery IsolateOperatorsForTZReassign(IsolateOperatorsForTZReassignQuery query);
        IsolatePanelsForTZDeleteQuery IsolatePanelsForTZDelete(IsolatePanelsForTZDeleteQuery query);
        IsolateAccessLevelsForTZReassignQuery IsolateAccessLevelsForTZReassign(IsolateAccessLevelsForTZReassignQuery query);
        IsolateActionGroupsForTZReassignQuery IsolateActionGroupsForTZReassign(IsolateActionGroupsForTZReassignQuery query);
        IsolateCardsForTZReassignQuery IsolateCardsForTZReassign(IsolateCardsForTZReassignQuery query);
        IsolateADVsForTZReassignQuery IsolateADVsForTZReassign(IsolateADVsForTZReassignQuery query);
        GetTZsForOperatorReassignQuery GetTZsForOperatorReassign(GetTZsForOperatorReassignQuery query);
        GetTZsForReassignQuery GetTZsForReassign(GetTZsForReassignQuery query);
        DeletePanelTZQuery DeletePanelTZ(DeletePanelTZQuery query);
        ReassignOperatorTZQuery ReassignOperatorTZ(ReassignOperatorTZQuery query);
        ReassignAccessLevelTZQuery ReassignAccessLevelTZ(ReassignAccessLevelTZQuery query);
        ReassignActionGroupTZQuery ReassignActionGroupTZ(ReassignActionGroupTZQuery query);
        ReassignCardTZQuery ReassignCardTZ(ReassignCardTZQuery query);
        ReassignADVTZQuery ReassignADVTZ(ReassignADVTZQuery query);
        AddHolidayQuery AddHoliday(AddHolidayQuery query);
        EditHolidayQuery EditHoliday(EditHolidayQuery query);
        DeleteHolidayQuery DeleteHoliday(DeleteHolidayQuery query);
        AddHolidayGroupQuery AddHolidayGroup(AddHolidayGroupQuery query);
        EditHolidayGroupQuery EditHolidayGroup(EditHolidayGroupQuery query);
        DeleteHolidayGroupQuery DeleteHolidayGroup(DeleteHolidayGroupQuery query);
        GetHolidayGroupsByAcctIDQuery GetHolidayGroupsByAcctID(GetHolidayGroupsByAcctIDQuery query);
        GetHolidaysByHolidayGroupIDQuery GetHolidaysByHolidayGroupID(GetHolidaysByHolidayGroupIDQuery query);
        GetHolidayByIDQuery GetHolidayByID(GetHolidayByIDQuery query);
        ConfigurePanelHolidayGroupQuery ConfigurePanelHolidayGroup(ConfigurePanelHolidayGroupQuery query);
        GetConfiguredHolidayGroupsByPanelQuery GetConfiguredHolidayGroupsByPanel(GetConfiguredHolidayGroupsByPanelQuery query);
        GetDirectPointTZDetailsofReaderQuery GetDirectPointTZDetailsofReader(GetDirectPointTZDetailsofReaderQuery query);
        ConnectWPDatabaseQuery ConnectWPDatabase(ConnectWPDatabaseQuery query);
        DisconnectWPDatabaseQuery DisconnectWPDatabase(DisconnectWPDatabaseQuery query);
        DisconnectQuery Disconnect(DisconnectQuery query);
        BulkDeleteCardsQuery BulkDeleteCards(BulkDeleteCardsQuery query);
        BulkAddCardsQuery BulkAddCards(BulkAddCardsQuery query);
        AddUpdateCardQuery AddUpdateCard(AddUpdateCardQuery query);
        AddUpdateALQuery AddUpdateAL(AddUpdateALQuery query);
        GetPhotoSizeQuery GetPhotoSize(GetPhotoSizeQuery query);
        GetPhotoQuery GetPhoto(GetPhotoQuery query);
        GetSigSizeQuery GetSigSize(GetSigSizeQuery query);
        GetSigQuery GetSig(GetSigQuery query);
        DeleteSigQuery DeleteSig(DeleteSigQuery query);
        ImportPhotoQuery ImportPhoto(ImportPhotoQuery query);
        ImportSigQuery ImportSig(ImportSigQuery query);
        IsConnectedQuery IsConnected(IsConnectedQuery query);
        IsGroupCheckedQuery IsGroupChecked(IsGroupCheckedQuery query);
        GetWPDBServerTZoffsetQuery GetWPDBServerTZoffset(GetWPDBServerTZoffsetQuery query);
        GetReaderTZDetailsByAccountIdQuery GetReaderTZDetailsByAccountId(GetReaderTZDetailsByAccountIdQuery query);
        GetWPDBServerTZQuery GetWPDBServerTZ(GetWPDBServerTZQuery query);
        GetAvailableGroupsofReaderQuery GetAvailableGroupsofReader(GetAvailableGroupsofReaderQuery query);
        GetAssociatedGroupofReaderQuery GetAssociatedGroupofReader(GetAssociatedGroupofReaderQuery query);
        ConfigureEntranceAccessQuery ConfigureEntranceAccess(ConfigureEntranceAccessQuery query);
        GetAvailableTimeZonesOfAccessReaderQuery GetAvailableTimeZonesOfAccessReader(GetAvailableTimeZonesOfAccessReaderQuery query);
        GetCurrentOperatorQuery GetCurrentOperator(GetCurrentOperatorQuery query);
        GetConfiguredTimezonesByPanelQuery GetConfiguredTimezonesByPanel(GetConfiguredTimezonesByPanelQuery query);
        GetAllAccessLevelsQuery GetAllAccessLevels(GetAllAccessLevelsQuery query);
        GetAllTimezonesQuery GetAllTimezones(GetAllTimezonesQuery query);
        CreateAccessLevelQuery CreateAccessLevel(CreateAccessLevelQuery query);
        CreateTimezoneQuery CreateTimezone(CreateTimezoneQuery query);
        IsolatePanelsForHGDeleteQuery IsolatePanelsForHGDelete(IsolatePanelsForHGDeleteQuery query);
        GetAccessTreeByNameQuery GetAccessTreeByName(GetAccessTreeByNameQuery query);
        GetCardHolderSearchFieldsByAccountNameQuery GetCardHolderSearchFieldsByAccountName(GetCardHolderSearchFieldsByAccountNameQuery query);
        GetCardHoldersOnSearchQuery GetCardHoldersOnSearch(GetCardHoldersOnSearchQuery query);
        GetConfiguredWPDomainsQuery GetConfiguredWPDomains(GetConfiguredWPDomainsQuery query);
        ConfigureOutputTimezoneExQuery ConfigureOutputTimezoneEx(ConfigureOutputTimezoneExQuery query);
        GetAssociatedTimezoneOfOutputExQuery GetAssociatedTimezoneOfOutputEx(GetAssociatedTimezoneOfOutputExQuery query);
        void LoadClass();
    }
}

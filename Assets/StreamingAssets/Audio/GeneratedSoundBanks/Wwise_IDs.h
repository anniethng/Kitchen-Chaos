/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID PLAY_CLICK = 311910498U;
        static const AkUniqueID PLAY_COLLISION = 1553219250U;
        static const AkUniqueID PLAY_IMPACT = 2764105556U;
        static const AkUniqueID PLAY_MUSIC = 2932040671U;
        static const AkUniqueID PLAY_TIMER = 4169444829U;
        static const AkUniqueID SET_STATE_INGAME = 3300611779U;
        static const AkUniqueID SET_STATE_MENU = 2302270969U;
        static const AkUniqueID STOP_TIMER = 3090597779U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAMESTATE
        {
            static const AkUniqueID GROUP = 4091656514U;

            namespace STATE
            {
                static const AkUniqueID INGAME = 984691642U;
                static const AkUniqueID MENU = 2607556080U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace GAMESTATE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace COLLISIONTYPE
        {
            static const AkUniqueID GROUP = 280300163U;

            namespace SWITCH
            {
                static const AkUniqueID HARD = 3599861390U;
                static const AkUniqueID SOFT = 670602561U;
            } // namespace SWITCH
        } // namespace COLLISIONTYPE

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID STRESSLEVEL = 2715298845U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MAIN_AUDIO_BUS = 2246998526U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__

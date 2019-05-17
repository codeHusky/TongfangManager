// Decompiled with JetBrains decompiler
// Type: LightingModel.LM_EC_RGB
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

namespace LightingModel
{
    public class LM_EC_RGB : ILM_RGBKB
  {
    private RGBKB_Type m_EC_KB_Type;

    public bool ILM_RGBKB_Init(RGBKB_Event_Handler event_handler)
    {
      return false;
    }

    public bool ILM_RGBKB_SetPower(RGBKB_PowerStatus PowerStatus)
    {
      return false;
    }

    public bool ILM_RGBKB_GetPower(ref RGBKB_PowerStatus PowerStatus)
    {
      return false;
    }

    public RGBKB_Type ILM_RGBKB_GetRGBKeyboardType()
    {
      return this.m_EC_KB_Type;
    }

    public string ILM_RGBKB_GetFirmwareVersion()
    {
      return "";
    }

    public bool ILM_RGBKB_SetEffectALL(
      RGBKB_Mode layout_mode,
      RGBKB_Effect layout_effect,
      uint layout_light,
      uint layout_speed,
      RGBKB_Direction layout_direction,
      RGBKB_Color layout_color,
      RGBKB_NV_SAVE layout_save)
    {
      return false;
    }

    public bool ILM_RGBKB_GetEffectALL(
      RGBKB_Mode layout_mode,
      ref RGBKB_Effect layout_effect,
      ref uint layout_light,
      ref uint layout_speed,
      ref RGBKB_Direction layout_direction,
      ref RGBKB_Color layout_color)
    {
      return false;
    }

    public bool ILM_RGBKB_SetEffect(RGBKB_Mode layout_mode, RGBKB_Effect layout_effect)
    {
      return false;
    }

    public bool ILM_RGBKB_GetEffect(RGBKB_Mode layout_mode, ref RGBKB_Effect layout_effect)
    {
      return false;
    }

    public bool ILM_RGBKB_SetBrighntess(uint layout_brightness)
    {
      return false;
    }

    public bool ILM_RGBKB_GetBrighntess(ref uint layout_brightness)
    {
      return false;
    }

    public bool ILM_RGBKB_SetSpeed(uint layout_speed)
    {
      return false;
    }

    public bool ILM_RGBKB_SetDirection(uint layout_direction)
    {
      return false;
    }

    public bool ILM_RGBKB_SetColor(
      RGBKB_Mode layout_mode,
      RGBKB_Effect layout_effect,
      RGBKB_Color layout_color)
    {
      return false;
    }

    public bool ILM_RGBKB_SaveLightingLevel(uint layout_light)
    {
      return false;
    }
  }
}

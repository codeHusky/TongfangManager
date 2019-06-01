// Decompiled with JetBrains decompiler
// Type: LightingModel.LM_Manager
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using Utility;

namespace LightingModel
{
  public class LM_Manager
  {
      public bool m_bInitStatus;
    public ILM_RGBKB m_ILM_RGBKB;
    public LM_ITE_RGB m_LM_ITE_RGB;
    public LM_EC_RGB m_LM_EC_RGB;
    public RGBKB_Solution m_KB_Solution;
    public RGBKB_Type m_KB_Type;

    public bool LM_Init(RGBKB_Event_Handler event_handler)
    {
      try
      {
        this.m_LM_ITE_RGB = new LM_ITE_RGB();
        this.m_LM_EC_RGB = new LM_EC_RGB();
        if (this.m_LM_ITE_RGB.ILM_RGBKB_Init(event_handler))
        {
          Log.s(LOG_LEVEL.INIT, "LM_Manager|LM_Init : ITE solution");
          this.m_KB_Solution = RGBKB_Solution.ITE;
          this.m_ILM_RGBKB = (ILM_RGBKB) this.m_LM_ITE_RGB;
          this.m_KB_Type = this.m_ILM_RGBKB.ILM_RGBKB_GetRGBKeyboardType();
          this.m_bInitStatus = true;
        }
        else if (this.m_LM_EC_RGB.ILM_RGBKB_Init(event_handler))
        {
          Log.s(LOG_LEVEL.INIT, "LM_Manager|LM_Init : EC solution");
          this.m_KB_Solution = RGBKB_Solution.EC;
          this.m_ILM_RGBKB = (ILM_RGBKB) this.m_LM_EC_RGB;
          this.m_KB_Type = RGBKB_Type.SingleZone;
          this.m_bInitStatus = true;
        }
        else
        {
          Log.s(LOG_LEVEL.INIT, "LM_Manager|LM_Init : Not RGB Keyboard moudle");
          this.m_bInitStatus = false;
        }
      }
      catch
      {
        this.m_bInitStatus = false;
      }
      return this.m_bInitStatus;
    }

    public bool LM_DeInit()
    {
      this.m_bInitStatus = false;
      return false;
    }

    public bool LM_SetPower(RGBKB_PowerStatus powerStatus)
    {
      if (!this.m_bInitStatus)
        return false;
      return this.m_ILM_RGBKB.ILM_RGBKB_SetPower(powerStatus);
    }

    public bool LM_SetPowerSaving(RGBKB_PowerStatus powerStatus)
    {
      int num = this.m_bInitStatus ? 1 : 0;
      return false;
    }

    public bool LM_SetEffectALL(
      RGBKB_Mode layout_mode,
      RGBKB_Effect layout_effect,
      uint layout_light,
      uint layout_speed,
      RGBKB_Direction layout_direction,
      RGBKB_Color layout_color,
      RGBKB_NV_SAVE layout_save)
    {
      if (!this.m_bInitStatus)
        return false;
      return this.m_ILM_RGBKB.ILM_RGBKB_SetEffectALL(layout_mode, layout_effect, layout_light, layout_speed, layout_direction, layout_color, layout_save);
    }

    public bool LM_SetColor(
      RGBKB_Mode layout_mode,
      RGBKB_Effect layout_effect,
      RGBKB_Color layout_color)
    {
      if (!this.m_bInitStatus)
        return false;
      return this.m_ILM_RGBKB.ILM_RGBKB_SetColor(layout_mode, layout_effect, layout_color);
    }

    public bool LM_GetEffect(RGBKB_Mode query_mode, ref RGBKB_Effect current_effect)
    {
      if (!this.m_bInitStatus)
        return false;
      return this.m_ILM_RGBKB.ILM_RGBKB_GetEffect(query_mode, ref current_effect);
    }

    public bool LM_SetLightingLevel(uint level)
    {
      if (!this.m_bInitStatus)
        return false;
      return this.m_ILM_RGBKB.ILM_RGBKB_SetBrighntess(level);
    }

    public bool LM_GetLightingLevel(ref uint level)
    {
      if (!this.m_bInitStatus)
        return false;
      return this.m_ILM_RGBKB.ILM_RGBKB_GetBrighntess(ref level);
    }

    public string LM_GetFirmwareVersion()
    {
      if (!this.m_bInitStatus)
        return "";
      return this.m_ILM_RGBKB.ILM_RGBKB_GetFirmwareVersion();
    }

    public bool LM_SaveLightingLevel(uint level)
    {
      if (!this.m_bInitStatus)
        return false;
      return this.m_ILM_RGBKB.ILM_RGBKB_SaveLightingLevel(level);
    }
  }
}

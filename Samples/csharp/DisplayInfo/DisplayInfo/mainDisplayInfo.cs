// 
// Copyright (c) 2021 - 2025 Advanced Micro Devices, Inc. All rights reserved.
//
//-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayInfo
{
    class mainDisplayInfo
    {
        static void Main(string[] args) {

            // Initialize ADLX with ADLXHelper
            ADLXHelper help = new ADLXHelper();
            ADLX_RESULT res = help.Initialize();

            if (res == ADLX_RESULT.ADLX_OK)
            {
                // Get system services
                IADLXSystem sys = help.GetSystemServices();

                if (sys != null)
                {
                    // Get display services
                    SWIGTYPE_p_p_adlx__IADLXDisplayServices s = ADLX.new_displaySerP_Ptr();
                    res = sys.GetDisplaysServices(s);
                    IADLXDisplayServices displayService = ADLX.displaySerP_Ptr_value(s);

                    if (res == ADLX_RESULT.ADLX_OK)
                    {
                        // Get display list
                        SWIGTYPE_p_p_adlx__IADLXDisplayList ppDisplayList = ADLX.new_displayListP_Ptr();
                        res = displayService.GetDisplays(ppDisplayList);
                        IADLXDisplayList displayList = ADLX.displayListP_Ptr_value(ppDisplayList);

                        if (res == ADLX_RESULT.ADLX_OK)
                        {
                            // Iterate through the display list
                            uint it = displayList.Begin();
                            for (; it != displayList.Size(); it++)
                            {
                                SWIGTYPE_p_p_adlx__IADLXDisplay ppDisplay = ADLX.new_displayP_Ptr();
                                res = displayList.At(it, ppDisplay);
                                IADLXDisplay display = ADLX.displayP_Ptr_value(ppDisplay);

                                if (res == ADLX_RESULT.ADLX_OK)
                                {
                                    SWIGTYPE_p_p_char ppName = ADLX.new_charP_Ptr();
                                    display.Name(ppName);
                                    String name = ADLX.charP_Ptr_value(ppName);

                                    SWIGTYPE_p_ADLX_DISPLAY_TYPE pDisType = ADLX.new_displayTypeP();
                                    display.DisplayType(pDisType);
                                    ADLX_DISPLAY_TYPE disType = ADLX.displayTypeP_value(pDisType);

                                    SWIGTYPE_p_unsigned_int pMID = ADLX.new_uintP();
                                    display.ManufacturerID(pMID);
                                    long mid = ADLX.uintP_value(pMID);

                                    SWIGTYPE_p_ADLX_DISPLAY_CONNECTOR_TYPE pConnect = ADLX.new_disConnectTypeP();
                                    display.ConnectorType(pConnect);
                                    ADLX_DISPLAY_CONNECTOR_TYPE connect = ADLX.disConnectTypeP_value(pConnect);

                                    SWIGTYPE_p_p_char ppEDIE = ADLX.new_charP_Ptr();
                                    display.EDID(ppEDIE);
                                    String edid = ADLX.charP_Ptr_value(ppEDIE);

                                    SWIGTYPE_p_int pH = ADLX.new_intP();
                                    SWIGTYPE_p_int pV = ADLX.new_intP();
                                    display.NativeResolution(pH, pV);
                                    int h = ADLX.intP_value(pH);
                                    int v = ADLX.intP_value(pV);

                                    SWIGTYPE_p_double pRefRate = ADLX.new_doubleP();
                                    display.RefreshRate(pRefRate);
                                    double refRate = ADLX.doubleP_value(pRefRate);

                                    SWIGTYPE_p_unsigned_int pPixClock = ADLX.new_uintP();
                                    display.PixelClock(pPixClock);
                                    long pixClock = ADLX.uintP_value(pPixClock);

                                    SWIGTYPE_p_ADLX_DISPLAY_SCAN_TYPE pScanType = ADLX.new_disScanTypeP();
                                    display.ScanType(pScanType);
                                    ADLX_DISPLAY_SCAN_TYPE scanType = ADLX.disScanTypeP_value(pScanType);

                                    SWIGTYPE_p_size_t pID = ADLX.new_adlx_sizeP();
                                    display.UniqueId(pID);
                                    uint id = ADLX.adlx_sizeP_value(pID);

                                    Console.WriteLine(String.Format("\nThe display [{0}]:", it));
                                    Console.WriteLine(String.Format("\tName: {0}", name));
                                    Console.WriteLine(String.Format("\tType: {0}", disType));
                                    Console.WriteLine(String.Format("\tConnector type: {0}", connect));
                                    Console.WriteLine(String.Format("\tManufacturer id: {0}", mid));
                                    Console.WriteLine(String.Format("\tEDID: {0}", edid));
                                    Console.WriteLine(String.Format("\tResolution:  h: {0}  v: {1}", h, v));
                                    Console.WriteLine(String.Format("\tRefresh rate: {0}", refRate));
                                    Console.WriteLine(String.Format("\tPixel clock: {0}", pixClock));
                                    Console.WriteLine(String.Format("\tScan type: {0}", scanType));
                                    Console.WriteLine(String.Format("\tUnique id: {0}", id));

                                    // Release display interface
                                    display.Release();
                                }
                            }

                            // Release display list interface
                            displayList.Release();
                        }

                        // Release display services interface
                        displayService.Release();
                    }

                    // Add sample for performance monitoring services.
                    SWIGTYPE_p_p_adlx__IADLXPerformanceMonitoringServices performanceMonitoringServicesPointer = ADLX.new_performanceMonitoringSerP_Ptr();
                    res = sys.GetPerformanceMonitoringServices(performanceMonitoringServicesPointer);
                    IADLXPerformanceMonitoringServices performanceMonitoringServices = ADLX.performanceMonitoringSerP_Ptr_value(performanceMonitoringServicesPointer);

                    // 3D Settings Services
                    SWIGTYPE_p_p_adlx__IADLX3DSettingsServices threeDSettingsServicesPointer = ADLX.new_threeDSettingsSerP_Ptr();
                    sys.Get3DSettingsServices(threeDSettingsServicesPointer);
                    IntPtr cPtr = ADLXPINVOKE.threeDSettingsSerP_Ptr_value(SWIGTYPE_p_p_adlx__IADLX3DSettingsServices.getCPtr(threeDSettingsServicesPointer));
                    IADLX3DSettingsServices1 threeDSettingsServices1 = new IADLX3DSettingsServices1(cPtr, false);
                    SWIGTYPE_p_p_adlx__IADLX3DAMDFluidMotionFrames threeDAMDFluidMotionFramesPointer = ADLX.new_threeDAMDFluidMotionFramesP_Ptr();
                    threeDSettingsServices1.GetAMDFluidMotionFrames(threeDAMDFluidMotionFramesPointer);
                    IADLX3DAMDFluidMotionFrames threeDAMDFluidMotionFrames = ADLX.threeDAMDFluidMotionFramesP_Ptr_value(threeDAMDFluidMotionFramesPointer);
                    //threeDAMDFluidMotionFrames.SetEnabled(false);
                    SWIGTYPE_p_bool afmfEnabledPointer = ADLX.new_boolP();
                    threeDAMDFluidMotionFrames.IsEnabled(afmfEnabledPointer);
                    bool afmfEnabled = ADLX.boolP_value(afmfEnabledPointer);
                    Console.WriteLine($"AMD Fluid Motion Frames Enabled={afmfEnabled}");
                    threeDAMDFluidMotionFrames.Release();
                    
                    if (res == ADLX_RESULT.ADLX_OK)
                    {
                        SWIGTYPE_p_p_adlx__IADLXSystemMetricsSupport systemMetricsSupportPointer = ADLX.new_systemMetricsSupportP_Ptr();
                        res = performanceMonitoringServices.GetSupportedSystemMetrics(systemMetricsSupportPointer);
                        if (res == ADLX_RESULT.ADLX_OK)
                        {
                            IADLXSystemMetricsSupport systemMetricSupport = ADLX.systemMetricsSupportP_Ptr_value(systemMetricsSupportPointer);
                            SWIGTYPE_p_bool pSupportedCPUUsage = ADLX.new_boolP();
                            ADLX_RESULT checkCPUUsageSupportedResult = systemMetricSupport.IsSupportedCPUUsage(pSupportedCPUUsage);
                            if (checkCPUUsageSupportedResult == ADLX_RESULT.ADLX_OK)
                            {
                                bool isSupportedCPUUsage = ADLX.boolP_value(pSupportedCPUUsage);
                                Console.WriteLine($"{(isSupportedCPUUsage ? "Support" : "Doesn't support")} CPU usage");
                            }
                            else
                            {
                                Console.WriteLine("Can't determine CPU usage support");
                            }
                            systemMetricSupport.Release();
                        }
                        else
                        {
                            Console.WriteLine("Can't get supported system metrics");
                        }

                        SWIGTYPE_p_p_adlx__IADLXFPSList fpsListPointer = ADLX.new_fpsListP_Ptr();
                        res = performanceMonitoringServices.GetFPSHistory(0, 5, fpsListPointer);
                        if (res == ADLX_RESULT.ADLX_OK)
                        {
                            IADLXFPSList fpsList = ADLX.fpsListP_Ptr_value(fpsListPointer);
                            Console.WriteLine($"Got FPS list {fpsList.Size()}");
                            fpsList.Release();
                        }
                        else
                        {
                            Console.WriteLine("Can't get FPS list");
                        }

                        SWIGTYPE_p_int performanceMetricsHistorySizePointer = ADLX.new_intP();
                        performanceMonitoringServices.GetCurrentPerformanceMetricsHistorySize(performanceMetricsHistorySizePointer);
                        int performanceMetricsHistorySize = ADLX.intP_value(performanceMetricsHistorySizePointer);
                        SWIGTYPE_p_p_adlx__IADLXAllMetricsList allMetricsListPointer = ADLX.new_allMetricsListP_Ptr();
                        res = performanceMonitoringServices.GetAllMetricsHistory(0, 5, allMetricsListPointer);
                        if (res == ADLX_RESULT.ADLX_OK)
                        {
                            IADLXAllMetricsList allMetricsList = ADLX.allMetricsListP_Ptr_value(allMetricsListPointer);
                            Console.WriteLine($"All metrics list Size={allMetricsList.Size()}");
                            allMetricsList.Release();
                            //allMetricsList.QueryInterface()
                        }
                        else
                        {
                            Console.WriteLine($"Can't get all metrics list, PerformanceMetricsHistorySize={performanceMetricsHistorySize}");
                        }

                        SWIGTYPE_p_p_adlx__IADLXAllMetrics allMetricsPointer = ADLX.new_allMetricsP_Ptr();
                        performanceMonitoringServices.GetCurrentAllMetrics(allMetricsPointer);
                        IADLXAllMetrics allMetrics = ADLX.allMetricsP_Ptr_value(allMetricsPointer);

                        SWIGTYPE_p_long_long timestampPointer = ADLX.new_int64P();
                        allMetrics.TimeStamp(timestampPointer);
                        long timeStamp = ADLX.int64P_value(timestampPointer);

                        SWIGTYPE_p_p_adlx__IADLXFPS adlxFPSPointer = ADLX.new_fpsP_Ptr();
                        allMetrics.GetFPS(adlxFPSPointer);
                        IADLXFPS adlxFPS = ADLX.fpsP_Ptr_value(adlxFPSPointer);
                        SWIGTYPE_p_int fpsPointer = ADLX.new_intP();
                        adlxFPS.FPS(fpsPointer);
                        int fps = ADLX.intP_value(fpsPointer);

                        SWIGTYPE_p_p_adlx__IADLXSystemMetrics systemMetricsPointer = ADLX.new_systemMetricsP_Ptr();
                        allMetrics.GetSystemMetrics(systemMetricsPointer);
                        IADLXSystemMetrics systemMetrics = ADLX.systemMetricsP_Ptr_value(systemMetricsPointer);

                        SWIGTYPE_p_double cpuUsagePointer = ADLX.new_doubleP();
                        systemMetrics.CPUUsage(cpuUsagePointer);
                        double cpuUsage = ADLX.doubleP_value(cpuUsagePointer);

                        SWIGTYPE_p_int smartShiftPointer = ADLX.new_intP();
                        systemMetrics.SmartShift(smartShiftPointer);
                        int smartShift = ADLX.intP_value(smartShiftPointer);

                        SWIGTYPE_p_int systemRAMPointer = ADLX.new_intP();
                        systemMetrics.SystemRAM(systemRAMPointer);
                        int systemRAM = ADLX.intP_value(systemRAMPointer);

                        Console.WriteLine($"[{timeStamp}] FPS={fps} CPUUsage={cpuUsage} SmartShift={smartShift} RAM={systemRAM}");

                        SWIGTYPE_p_p_adlx__IADLXGPUList gpuListPointer = ADLX.new_gpuListP_Ptr();
                        sys.GetGPUs(gpuListPointer);
                        IADLXGPUList gpuList = ADLX.gpuListP_Ptr_value(gpuListPointer);
                        for (uint i = 0; i < gpuList.Size(); i++)
                        {
                            SWIGTYPE_p_p_adlx__IADLXGPU gpuPointer = ADLX.new_gpuP_Ptr();
                            gpuList.At(i, gpuPointer);
                            IADLXGPU gpu = ADLX.gpuP_Ptr_value(gpuPointer);

                            SWIGTYPE_p_p_adlx__IADLXGPUMetrics gpuMetricsPointer = ADLX.new_gpuMetricsP_Ptr();
                            allMetrics.GetGPUMetrics(gpu, gpuMetricsPointer);
                            IADLXGPUMetrics gpuMetrics = ADLX.gpuMetricsP_Ptr_value(gpuMetricsPointer);

                            SWIGTYPE_p_long_long gpuTimestampPointer = ADLX.new_int64P();
                            gpuMetrics.TimeStamp(gpuTimestampPointer);
                            long gpuTimeStamp = ADLX.int64P_value(gpuTimestampPointer);

                            SWIGTYPE_p_int gpuClockSpeedPointer = ADLX.new_intP();
                            gpuMetrics.GPUClockSpeed(gpuClockSpeedPointer);
                            int gpuClockSpeed = ADLX.intP_value(gpuClockSpeedPointer);

                            SWIGTYPE_p_int gpuFanSpeedPointer = ADLX.new_intP();
                            gpuMetrics.GPUFanSpeed(gpuFanSpeedPointer);
                            int gpuFanSpeed = ADLX.intP_value(gpuFanSpeedPointer);

                            SWIGTYPE_p_double gpuHotspotTemperaturePointer = ADLX.new_doubleP();
                            gpuMetrics.GPUHotspotTemperature(gpuHotspotTemperaturePointer);
                            double gpuHotspotTemperature = ADLX.doubleP_value(gpuHotspotTemperaturePointer);

                            SWIGTYPE_p_double gpuIntakeTemperaturePointer = ADLX.new_doubleP();
                            gpuMetrics.GPUIntakeTemperature(gpuIntakeTemperaturePointer);
                            double gpuIntakeTemperature = ADLX.doubleP_value(gpuIntakeTemperaturePointer);

                            SWIGTYPE_p_double gpuPowerPointer = ADLX.new_doubleP();
                            gpuMetrics.GPUPower(gpuPowerPointer);
                            double gpuPower = ADLX.doubleP_value(gpuPowerPointer);

                            SWIGTYPE_p_double gpuTemperaturePointer = ADLX.new_doubleP();
                            gpuMetrics.GPUTemperature(gpuTemperaturePointer);
                            double gpuTemperature = ADLX.doubleP_value(gpuTemperaturePointer);

                            SWIGTYPE_p_double gpuTotalBoardPowerPointer = ADLX.new_doubleP();
                            gpuMetrics.GPUTotalBoardPower(gpuTotalBoardPowerPointer);
                            double gpuTotalBoardPower = ADLX.doubleP_value(gpuTotalBoardPowerPointer);

                            SWIGTYPE_p_double gpuUsagePointer = ADLX.new_doubleP();
                            gpuMetrics.GPUUsage(gpuUsagePointer);
                            double gpuUsage = ADLX.doubleP_value(gpuUsagePointer);

                            SWIGTYPE_p_int gpuVoltagePointer = ADLX.new_intP();
                            gpuMetrics.GPUVoltage(gpuVoltagePointer);
                            int gpuVoltage = ADLX.intP_value(gpuVoltagePointer);

                            SWIGTYPE_p_int gpuVRAMPointer = ADLX.new_intP();
                            gpuMetrics.GPUVRAM(gpuVRAMPointer);
                            int gpuVRAM = ADLX.intP_value(gpuVRAMPointer);

                            SWIGTYPE_p_int gpuVRAMClockSpeedPointer = ADLX.new_intP();
                            gpuMetrics.GPUVRAMClockSpeed(gpuVRAMClockSpeedPointer);
                            int gpuVRAMClockSpeed = ADLX.intP_value(gpuVRAMClockSpeedPointer);

                            SWIGTYPE_p_p_adlx__IADLXGPUMetricsList gpuMetricsHistoryPointer = ADLX.new_gpuMetricsListP_Ptr();
                            res = performanceMonitoringServices.GetGPUMetricsHistory(gpu, 0, 5, gpuMetricsHistoryPointer);
                            if (res == ADLX_RESULT.ADLX_OK)
                            {
                                IADLXGPUMetricsList gpuMetricsHistory = ADLX.gpuMetricsListP_Ptr_value(gpuMetricsHistoryPointer);
                                Console.WriteLine($"GPU metric history {gpuMetricsHistory.Size()}");
                            }
                            else
                            {
                                Console.WriteLine("Can't get GPU metric history");
                            }

                            Console.WriteLine($"[{gpuTimeStamp}] GPU {i} ClockSpeed={gpuClockSpeed} FanSpeed={gpuFanSpeed} HotspotTemperature={gpuHotspotTemperature} IntakeTemperature={gpuIntakeTemperature} Power={gpuPower} Temperature={gpuTemperature} TotalBoardPower={gpuTotalBoardPower} Usage={gpuUsage} Voltage={gpuVoltage} VRAM={gpuVRAM} VRAMClockSpeed={gpuVRAMClockSpeed}");
                            gpuMetrics.Release();

                            SWIGTYPE_p_p_adlx__IADLX3DSettingsChangedHandling threeDSettingsChangedHandlingPointer = ADLX.new_threeDSettingsChangedHandlingP_Ptr();
                            threeDSettingsServices1.Get3DSettingsChangedHandling(threeDSettingsChangedHandlingPointer);
                            IADLX3DSettingsChangedHandling threeDSettingsChangedHandling = ADLX.threeDSettingsChangedHandlingP_Ptr_value(threeDSettingsChangedHandlingPointer);
                            //IADLX3DSettingsChangedListener threeDSettingsChangedListener = new IADLX3DSettingsChangedListener();
                            //threeDSettingsChangedHandling.Add3DSettingsEventListener(threeDSettingsChangedListener);
                            Console.WriteLine("Got 3DSettingsChangedHandling");
                            threeDSettingsChangedHandling.Release();

                            SWIGTYPE_p_p_adlx__IADLX3DAnisotropicFiltering threeDAnisotropicFilteringPointer = ADLX.new_threeDAnisotropicFilteringP_Ptr();
                            threeDSettingsServices1.GetAnisotropicFiltering(gpu, threeDAnisotropicFilteringPointer);
                            IADLX3DAnisotropicFiltering threeDAnisotropicFiltering = ADLX.threeDAnisotropicFilteringP_Ptr_value(threeDAnisotropicFilteringPointer);
                            SWIGTYPE_p_bool threeDAnisotropicFilteringIsSupportedPointer = ADLX.new_boolP();
                            threeDAnisotropicFiltering.IsSupported(threeDAnisotropicFilteringIsSupportedPointer);
                            bool threeDAnisotropicFilteringIsSupported = ADLX.boolP_value(threeDAnisotropicFilteringIsSupportedPointer);
                            SWIGTYPE_p_bool threeDAnisotropicFilteringIsEnabledPointer = ADLX.new_boolP();
                            threeDAnisotropicFiltering.IsEnabled(threeDAnisotropicFilteringIsEnabledPointer);
                            bool threeDAnisotropicFilteringIsEnabled = ADLX.boolP_value(threeDAnisotropicFilteringIsEnabledPointer);
                            SWIGTYPE_p_ADLX_ANISOTROPIC_FILTERING_LEVEL anisotropicFilteringLevelPointer = ADLX.new_anisotropicFilteringLevelP();
                            threeDAnisotropicFiltering.GetLevel(anisotropicFilteringLevelPointer);
                            ADLX_ANISOTROPIC_FILTERING_LEVEL anisotropicFilteringLevel = ADLX.anisotropicFilteringLevelP_value(anisotropicFilteringLevelPointer);
                            threeDAnisotropicFiltering.SetEnabled(false);
                            //threeDAnisotropicFiltering.SetLevel(ADLX_ANISOTROPIC_FILTERING_LEVEL.AF_LEVEL_X2);
                            Console.WriteLine($"AnisotropicFiltering Supported={threeDAnisotropicFilteringIsSupported} IsEnabled={threeDAnisotropicFilteringIsEnabled} Level={anisotropicFilteringLevel}");
                            threeDAnisotropicFiltering.Release();

                            SWIGTYPE_p_p_adlx__IADLX3DAntiAliasing threeDAntiAliasingPointer = ADLX.new_threeDAntiAliasingP_Ptr();
                            threeDSettingsServices1.GetAntiAliasing(gpu, threeDAntiAliasingPointer);
                            IADLX3DAntiAliasing threeDAntiAliasing = ADLX.threeDAntiAliasingP_Ptr_value(threeDAntiAliasingPointer);
                            SWIGTYPE_p_bool threeDAntiAliasingSupportedPointer = ADLX.new_boolP();
                            threeDAntiAliasing.IsSupported(threeDAntiAliasingSupportedPointer);
                            bool threeDAntiAliasingSupported = ADLX.boolP_value(threeDAntiAliasingSupportedPointer);
                            SWIGTYPE_p_ADLX_ANTI_ALIASING_LEVEL antiAliasingLevelPointer = ADLX.new_antiAliasingLevelP();
                            threeDAntiAliasing.GetLevel(antiAliasingLevelPointer);
                            ADLX_ANTI_ALIASING_LEVEL antiAliasingLevel = ADLX.antiAliasingLevelP_value(antiAliasingLevelPointer);
                            SWIGTYPE_p_ADLX_ANTI_ALIASING_METHOD antiAliasingMethodPointer = ADLX.new_antiAliasingMethodP();
                            threeDAntiAliasing.GetMethod(antiAliasingMethodPointer);
                            ADLX_ANTI_ALIASING_METHOD antiAliasingMethod = ADLX.antiAliasingMethodP_value(antiAliasingMethodPointer);
                            SWIGTYPE_p_ADLX_ANTI_ALIASING_MODE antiAliasingModePointer = ADLX.new_antiAliasingModeP();
                            threeDAntiAliasing.GetMode(antiAliasingModePointer);
                            ADLX_ANTI_ALIASING_MODE antiAliasingMode = ADLX.antiAliasingModeP_value(antiAliasingModePointer);
                            Console.WriteLine($"AntiAliasing Supported={threeDAntiAliasingSupported} Level={antiAliasingLevel} Method={antiAliasingMethod} Mode={antiAliasingMode}");
                            threeDAntiAliasing.Release();

                            SWIGTYPE_p_p_adlx__IADLX3DAntiLag threeDAntiLagPointer = ADLX.new_threeDAntiLagP_Ptr();
                            threeDSettingsServices1.GetAntiLag(gpu, threeDAntiLagPointer);
                            IADLX3DAntiLag threeDAntiLag = ADLX.threeDAntiLagP_Ptr_value(threeDAntiLagPointer);
                            threeDAntiLag.Release();

                            SWIGTYPE_p_p_adlx__IADLX3DBoost threeDBoostPointer = ADLX.new_threeDBoostP_Ptr();
                            threeDSettingsServices1.GetBoost(gpu, threeDBoostPointer);
                            IADLX3DBoost threeDBoost = ADLX.threeDBoostP_Ptr_value(threeDBoostPointer);
                            //threeDBoost.SetEnabled(true);
                            threeDBoost.Release();

                            SWIGTYPE_p_p_adlx__IADLX3DChill threeDChillPointer = ADLX.new_threeDChillP_Ptr();
                            threeDSettingsServices1.GetChill(gpu, threeDChillPointer);
                            IADLX3DChill threeDChill = ADLX.threeDChillP_Ptr_value(threeDChillPointer);
                            threeDChill.Release();

                            SWIGTYPE_p_p_adlx__IADLX3DEnhancedSync threeDEnhancedSyncPointer = ADLX.new_threeDEnhancedSyncP_Ptr();
                            threeDSettingsServices1.GetEnhancedSync(gpu, threeDEnhancedSyncPointer);
                            IADLX3DEnhancedSync threeDEnhancedSync = ADLX.threeDEnhancedSyncP_Ptr_value(threeDEnhancedSyncPointer);
                            //threeDEnhancedSync.SetEnabled(false);
                            threeDEnhancedSync.Release();

                            SWIGTYPE_p_p_adlx__IADLX3DFrameRateTargetControl threeDFrameRateTargetControlPointer = ADLX.new_threeDFrameRateTargetControlP_Ptr();
                            threeDSettingsServices1.GetFrameRateTargetControl(gpu, threeDFrameRateTargetControlPointer);
                            IADLX3DFrameRateTargetControl threeDFrameRateTargetControl = ADLX.threeDFrameRateTargetControlP_Ptr_value(threeDFrameRateTargetControlPointer);
                            //threeDFrameRateTargetControl.SetFPS(60);
                            threeDFrameRateTargetControl.Release();

                            SWIGTYPE_p_p_adlx__IADLX3DImageSharpening threeDImageSharpeningPointer = ADLX.new_threeDImageSharpeningP_Ptr();
                            threeDSettingsServices1.GetImageSharpening(gpu, threeDImageSharpeningPointer);
                            IADLX3DImageSharpening threeDImageSharpening = ADLX.threeDImageSharpeningP_Ptr_value(threeDImageSharpeningPointer);
                            //threeDImageSharpening.SetSharpness(50);
                            threeDImageSharpening.Release();

                            SWIGTYPE_p_p_adlx__IADLX3DMorphologicalAntiAliasing threeDMorphologicalAntiAliasingPointer = ADLX.new_threeDMorphologicalAntiAliasingP_Ptr();
                            threeDSettingsServices1.GetMorphologicalAntiAliasing(gpu, threeDMorphologicalAntiAliasingPointer);
                            IADLX3DMorphologicalAntiAliasing threeDMorphologicalAntiAliasing = ADLX.threeDMorphologicalAntiAliasingP_Ptr_value(threeDMorphologicalAntiAliasingPointer);
                            //threeDMorphologicalAntiAliasing.SetEnabled(false);
                            threeDMorphologicalAntiAliasing.Release();

                            SWIGTYPE_p_p_adlx__IADLX3DRadeonSuperResolution threeDRadeonSuperResolutionPointer = ADLX.new_threeDRadeonSuperResolutionP_Ptr();
                            threeDSettingsServices1.GetRadeonSuperResolution(threeDRadeonSuperResolutionPointer);
                            IADLX3DRadeonSuperResolution threeDRadeonSuperResolution = ADLX.threeDRadeonSuperResolutionP_Ptr_value(threeDRadeonSuperResolutionPointer);
                            //threeDRadeonSuperResolution.SetEnabled(false);
                            threeDRadeonSuperResolution.Release();

                            SWIGTYPE_p_p_adlx__IADLX3DResetShaderCache threeDResetShaderCachePointer = ADLX.new_threeDResetShaderCacheP_Ptr();
                            threeDSettingsServices1.GetResetShaderCache(gpu, threeDResetShaderCachePointer);
                            IADLX3DResetShaderCache threeDResetShaderCache = ADLX.threeDResetShaderCacheP_Ptr_value(threeDResetShaderCachePointer);
                            //threeDResetShaderCache.ResetShaderCache();
                            threeDResetShaderCache.Release();

                            SWIGTYPE_p_p_adlx__IADLX3DTessellation threeDTessellationPointer = ADLX.new_threeDTessellationP_Ptr();
                            threeDSettingsServices1.GetTessellation(gpu, threeDTessellationPointer);
                            IADLX3DTessellation threeDTessellation = ADLX.threeDTessellationP_Ptr_value(threeDTessellationPointer);
                            SWIGTYPE_p_ADLX_TESSELLATION_LEVEL tessellationLevelPointer = ADLX.new_tesselationLevelP();
                            threeDTessellation.GetLevel(tessellationLevelPointer);
                            ADLX_TESSELLATION_LEVEL tessellationLevel = ADLX.tesselationLevelP_value(tessellationLevelPointer);
                            SWIGTYPE_p_ADLX_TESSELLATION_MODE tessellationModePointer = ADLX.new_tesselationModeP();
                            threeDTessellation.GetMode(tessellationModePointer);
                            ADLX_TESSELLATION_MODE tessellationMode = ADLX.tesselationModeP_value(tessellationModePointer);
                            threeDTessellation.Release();

                            SWIGTYPE_p_p_adlx__IADLX3DWaitForVerticalRefresh threeDWaitForVerticalRefreshPointer = ADLX.new_threeDWaitForVerticalRefreshP_Ptr();
                            threeDSettingsServices1.GetWaitForVerticalRefresh(gpu, threeDWaitForVerticalRefreshPointer);
                            IADLX3DWaitForVerticalRefresh threeDWaitForVerticalRefresh = ADLX.threeDWaitForVerticalRefreshP_Ptr_value(threeDWaitForVerticalRefreshPointer);
                            SWIGTYPE_p_ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE waitForVerticalRefreshPointer = ADLX.new_waitForVerticalRefreshModeP();
                            threeDWaitForVerticalRefresh.GetMode(waitForVerticalRefreshPointer);
                            ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE waitForVerticalRefresh = ADLX.waitForVerticalRefreshModeP_value(waitForVerticalRefreshPointer);
                            threeDWaitForVerticalRefresh.Release();

                            gpu.Release();
                        }

                        ADLX_IntRange maxPerformanceMetricsHistorySizeRangePointer = ADLX.new_intRangeP();
                        res = performanceMonitoringServices.GetMaxPerformanceMetricsHistorySizeRange(maxPerformanceMetricsHistorySizeRangePointer);
                        if (res == ADLX_RESULT.ADLX_OK)
                        {
                            ADLX_IntRange maxPerformanceMetricsHistorySizeRange = ADLX.intRangeP_value(maxPerformanceMetricsHistorySizeRangePointer);
                            Console.WriteLine($"Got max performance metrics history size range {maxPerformanceMetricsHistorySizeRange.minValue} - {maxPerformanceMetricsHistorySizeRange.maxValue}");
                        }
                        else
                        {
                            Console.WriteLine("Can't get max performance metrics history size range");
                        }

                        allMetrics.Release();
                        systemMetrics.Release();
                        adlxFPS.Release();
                        gpuList.Release();
                        performanceMonitoringServices.Release();
                    }
                    else
                    {
                        Console.WriteLine("Can't get performance monitoring services");
                    }

                    threeDSettingsServices1.Release();
                }
            }
            else
            {
                Console.WriteLine(String.Format("ADLX helper init res:: {0}", res));
            }

            // Terminate ADLX
            res = help.Terminate();
            Console.WriteLine(String.Format("ADLX Terminate res: {0}", res));
            //Console.ReadKey();
        }
    }
}
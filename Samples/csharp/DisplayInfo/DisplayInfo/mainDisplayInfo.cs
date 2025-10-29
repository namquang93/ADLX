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
                    if (res == ADLX_RESULT.ADLX_OK)
                    {
                        IADLXPerformanceMonitoringServices performanceMonitoringServices = ADLX.performanceMonitoringSerP_Ptr_value(performanceMonitoringServicesPointer);
                        var systemMetricsSupportPointer = ADLX.new_systemMetricsSupportP_Ptr();
                        res = performanceMonitoringServices.GetSupportedSystemMetrics(systemMetricsSupportPointer);
                        if (res == ADLX_RESULT.ADLX_OK)
                        {
                            IADLXSystemMetricsSupport systemMetricSupport = ADLX.systemMetricsSupportP_Ptr_value(systemMetricsSupportPointer);
                            SWIGTYPE_p_bool pSupportedCPUUsage = ADLX.new_boolP();
                            ADLX_RESULT checkCPUUsageSupportedResult = systemMetricSupport.IsSupportedCPUUsage(pSupportedCPUUsage);
                            if (checkCPUUsageSupportedResult == ADLX_RESULT.ADLX_OK)
                            {
                                var isSupportedCPUUsage = ADLX.boolP_value(pSupportedCPUUsage);
                                Console.WriteLine($"{(isSupportedCPUUsage ? "Support" : "Doesn't support")} CPU usage");
                            }
                            else
                            {
                                Console.WriteLine("Can't determine CPU usage support");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Can't get supported system metrics");
                        }

                        SWIGTYPE_p_p_adlx__IADLXAllMetricsList allMetricsListPointer = ADLX.new_allMetricsListP_Ptr();
                        res = performanceMonitoringServices.GetAllMetricsHistory(0, 1000, allMetricsListPointer);
                        if (res == ADLX_RESULT.ADLX_OK)
                        {
                            IADLXAllMetricsList allMetricsList = ADLX.allMetricsListP_Ptr_value(allMetricsListPointer);
                            Console.WriteLine($"All metrics list Size={allMetricsList.Size()} Acquire={allMetricsList.Acquire()}");
                            //allMetricsList.QueryInterface()
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

                        //allMetrics.GetGPUMetrics()
                        //sys.GetGPUs()

                        Console.WriteLine($"[{timeStamp}] FPS={fps} CPUUsage={cpuUsage} SmartShift={smartShift} RAM={systemRAM}");
                    }
                    else
                    {
                        Console.WriteLine("Can't get performance monitoring services");
                    }
                }
            }
            else
            {
                Console.WriteLine(String.Format("ADLX helper init res:: {0}", res));
            }

            // Terminate ADLX
            res = help.Terminate();
            Console.WriteLine(String.Format("ADLX Terminate res: {0}", res));
            Console.ReadKey();
        }
    }
}
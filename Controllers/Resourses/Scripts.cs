﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingYourComputer.Controllers.Resourses
{
    public class Scripts
    {
        public string scriptMemory = @"# Based on System Management BIOS (SMBIOS) Reference Specification 3.4.0a
# https://www.dmtf.org/sites/default/files/standards/documents/DSP0134_3.4.0a.pdf

# 7.18.1. Form factor @offset 0x0E
[string[]]$FORM_FACTORS = @(
    'Invalid', 'Other', 'Unknown', 'SIMM',                      # 00-03h
    'SIP', 'Chip', 'DIP', 'ZIP'                                 # 04-07h
    'Proprietary Card', 'DIMM', 'TSOP', 'Row of chips',         # 08-0Bh
    'RIMM', 'SODIMM', 'SRIMM', 'FB-DIMM',                       # 0C-0Fh
    'Die'                                                       # 10h
)
# 7.18.2. Memory type @offset 0x12
[string[]]$MEMORY_TYPES = @(
    'Invalid',  'Other',    'Unknown',  'DRAM',                 # 00-03h
    'EDRAM',    'VRAM',     'SRAM',     'RAM',                  # 04-07h
    'ROM',      'FLASH',    'EEPROM',   'FEPROM',               # 08-0Bh
    'EPROM',    'CDRAM',    '3DRAM',    'SDRAM',                # 0C-0Fh
    'SGRAM',    'RDRAM',    'DDR',      'DDR2',                 # 10-13h
    'DDR2 FB-DIMM', 'Reserved', 'Reserved', 'Reserved',         # 14-17h
    'DDR3',     'FBD2',     'DDR4',     'LPDDR',                # 18-1Bh
    'LPDDR2',   'LPDDR3',   'LPDDR4',   'Logical non-volatile device' # 1C-1Fh
    'HBM (High Bandwidth Memory)', 'HBM2 (High Bandwidth Memory Generation 2)',
        'DDR5', 'LPDDR5'                                        # 20-23h
)
# 7.18.3. Type detail @offset 0x13
[string[]]$TYPE_DETAILS = @(
    'Reserved', 'Other', 'Unknown', 'Fast-paged',               # bit 0-3
    'Static column', 'Pseudo-static', 'RAMBUS', 'Synchronous',  # bit 4-7
    'CMOS', 'EDO', 'Window DRAM', 'Cache DRAM',                 # bit 8-11
    'Non-volatile', 'Registered (Buffered)',
        'Unbuffered (Unregistered)', 'LRDIMM'                   # 0C-0Fh
)

function lookUp([string[]]$table, [int]$value)
{
    if ($value -ge 0 -and $value -lt $table.Length) {
        $table[$value]
    } else {
        'Unknown value 0x{ 0:X}
            ' -f $value
    }
    }

    function parseTable([array]$table, [int]$begin, [int]$end)
    {
    [int]$index = $begin
    $size = [BitConverter]::ToUInt16($table, $index + 0x0C)
    if ($size -eq 0xFFFF) {
        'Unknown memory size'
    }
elseif($size -ne 0x7FFF)
{
        #if (($size -shr 15) -eq 0) { $size *= 1MB } else { $size *= 1KB }
         if ([Math]::Floor($size/32768) -eq 0) { $size *= 1MB } else { $size *= 1KB }
    } else {
        $size = [BitConverter]::ToUInt32($table, $index + 0x1C)
    }
    'Size, GB: {0}' -f ($size/1GB)

    $formFactor = $table[$index + 0x0E]
    $formFactorStr = $(lookUp $FORM_FACTORS $formFactor)
    'Memory form factor: 0x{0:X2} {1}' -f $formFactor, $formFactorStr

    $type = $table[$index + 0x12]
    'Memory type: 0x{0:X2} ({1})' -f $type, $(lookUp $MEMORY_TYPES $type)

    $typeDetail = [BitConverter]::ToUInt16($table, $index + 0x13)
    $details = 0..15 |% {
#if (((1 -shl $_) -band $typeDetail) -ne 0) { '{0}' -f $TYPE_DETAILS[$_] }
         if (([int]([Math]::Pow(2, $_)) -band $typeDetail) -ne 0) { '{0}' -f $TYPE_DETAILS[$_] }
    }
    'Type detail: 0x{0:X2} ({1})' -f $typeDetail, $($details -join ' | ')

    $speed = [BitConverter]::ToUInt16($table, $index + 0x15)
    if ($speed -eq 0) {
        'Unknown speed'
    } elseif ($speed -ne 0xFFFF) {
        'Speed, MHz: {0}' -f $speed
    } else {
        'Speed, MHz: {0}' -f [BitConverter]::ToUInt32($table, $index + 0x54)
    }
    '======================='
}

$index = 0

$END_OF_TABLES = 127
$MEMORY_DEVICE = 17

$BiosTables = (Get-WmiObject -ComputerName . -Namespace root\wmi -Query `
    'SELECT SMBiosData FROM MSSmBios_RawSMBiosTables' `
).SMBiosData

do
{
    $startIndex = $index

# ========= Parse table header =========
    $tableType = $BiosTables[$index]
    if ($tableType -eq $END_OF_TABLES) { break }

    $tableLength = $BiosTables[$index + 1]
# $tableHandle = [BitConverter]::ToUInt16($BiosTables, $index + 2)
    $index += $tableLength

# ========= Parse unformatted part =========
# Find the '\0\0' structure termination
    while ([BitConverter]::ToUInt16($BiosTables, $index) -ne 0) { $index++ }
    $index += 2

# adjustment when the table ends with a string
    if ($BiosTables[$index] -eq 0) { $index++ }

    if ($tableType -eq $MEMORY_DEVICE) { parseTable $BiosTables $startIndex $index }
} until ($tableType -eq $END_OF_TABLES -or $index -ge $BiosTables.length)";

        public string computerName = "SELECT * Win32_ComputerSystem";
        public string rootName = "root\\CIMV2";
        public string scriptProcessor = "SELECT * FROM Win32_Processor";
        public string scriptVideocard = "SELECT * FROM Win32_VideoController";
        public string scriptHardDrive = "SELECT * FROM Win32_DiskDrive";
        public string scriptOS = @"SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion";

    }
}

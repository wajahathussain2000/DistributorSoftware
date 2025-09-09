# Visual Studio Debug Guide for Distribution Software

## How to See Debug Messages in Visual Studio

### Method 1: Output Window (Recommended)
1. **Run the application in Debug mode** (F5)
2. **Open the Output window**: View → Output (or Ctrl+Alt+O)
3. **Select "Debug" from the dropdown** in the Output window
4. **Create a sales invoice** - you should see debug messages like:
   ```
   [DEBUG] === CREATE SALES INVOICE DEBUG START ===
   [DEBUG] Invoice Number: 2025090001
   [DEBUG] Customer ID: 1
   [DEBUG] Items Count: 2
   [DEBUG] Step 1: Validating invoice...
   [DEBUG] ✅ Invoice validation passed
   ```

### Method 2: Debug Output Window
1. **Run in Debug mode** (F5)
2. **Open Debug Output**: Debug → Windows → Output
3. **Select "Debug" from the Show output from dropdown**

### Method 3: Console Window (If available)
1. **Run in Debug mode** (F5)
2. **Look for Console window** in Visual Studio
3. **Debug messages will appear with [DEBUG] prefix**

## Debugger Breakpoints

The code now has automatic breakpoints at these locations:

1. **Before Database Call** (SalesInvoiceService.cs line 82)
   - Breaks before calling the repository
   - Inspect `invoice` object

2. **Before Database Execution** (SalesInvoiceRepository.cs line 135)
   - Breaks before ExecuteScalar
   - Inspect `command.Parameters`

3. **Exception Handling** (SalesInvoiceService.cs line 105)
   - Breaks when exception occurs
   - Inspect `ex` object

## Key Variables to Watch

When debugging, check these variables:
- `invoice.CustomerId` - Should be > 0 (or mapped to walk-in customer)
- `invoice.BarcodeImage` - Should not be null
- `invoice.Items.Count` - Should be > 0
- `command.Parameters["@CustomerId"].Value` - Should be valid customer ID
- `command.Parameters["@BarcodeImage"].Value` - Should be byte array or DBNull.Value

## Troubleshooting

### If you don't see debug messages:
1. **Make sure you're running in Debug mode** (not Release)
2. **Check the Output window dropdown** is set to "Debug"
3. **Try Console.WriteLine** - messages should appear in Output window
4. **Check if debugger is attached** - look for "Debug" in Visual Studio title bar

### If breakpoints don't work:
1. **Make sure symbols are loaded** - Debug → Windows → Modules
2. **Check if code is optimized** - Debug → Options → Debugging → General
3. **Disable "Just My Code"** if needed

## Testing Debug Output

To test if debug output is working:
1. **Run the application**
2. **Try to create a sales invoice**
3. **Look for messages starting with [DEBUG]**
4. **If you see them, debug output is working correctly**

## Debug Message Format

All debug messages follow this format:
- `[DEBUG] === CREATE SALES INVOICE DEBUG START ===` - Process start
- `[DEBUG] Step X: Description` - Process steps
- `[DEBUG] ✅ Success message` - Success indicators
- `[DEBUG] ❌ Error message` - Error indicators
- `[DEBUG] 🔍 Info message` - Information

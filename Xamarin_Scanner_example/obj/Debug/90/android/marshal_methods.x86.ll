; ModuleID = 'obj\Debug\90\android\marshal_methods.x86.ll'
source_filename = "obj\Debug\90\android\marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android"


%struct.MonoImage = type opaque

%struct.MonoClass = type opaque

%struct.MarshalMethodsManagedClass = type {
	i32,; uint32_t token
	%struct.MonoClass*; MonoClass* klass
}

%struct.MarshalMethodName = type {
	i64,; uint64_t id
	i8*; char* name
}

%class._JNIEnv = type opaque

%class._jobject = type {
	i8; uint8_t b
}

%class._jclass = type {
	i8; uint8_t b
}

%class._jstring = type {
	i8; uint8_t b
}

%class._jthrowable = type {
	i8; uint8_t b
}

%class._jarray = type {
	i8; uint8_t b
}

%class._jobjectArray = type {
	i8; uint8_t b
}

%class._jbooleanArray = type {
	i8; uint8_t b
}

%class._jbyteArray = type {
	i8; uint8_t b
}

%class._jcharArray = type {
	i8; uint8_t b
}

%class._jshortArray = type {
	i8; uint8_t b
}

%class._jintArray = type {
	i8; uint8_t b
}

%class._jlongArray = type {
	i8; uint8_t b
}

%class._jfloatArray = type {
	i8; uint8_t b
}

%class._jdoubleArray = type {
	i8; uint8_t b
}

; assembly_image_cache
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 4
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [124 x i32] [
	i32 39109920, ; 0: Newtonsoft.Json.dll => 0x254c520 => 4
	i32 57967248, ; 1: Xamarin.Android.Support.VersionedParcelable.dll => 0x3748290 => 46
	i32 160529393, ; 2: Xamarin.Android.Arch.Core.Common => 0x9917bf1 => 13
	i32 166922606, ; 3: Xamarin.Android.Support.Compat.dll => 0x9f3096e => 24
	i32 201930040, ; 4: Xamarin.Android.Arch.Core.Runtime => 0xc093538 => 14
	i32 232815796, ; 5: System.Web.Services => 0xde07cb4 => 59
	i32 293914992, ; 6: Xamarin.Android.Support.Transition => 0x1184c970 => 41
	i32 321597661, ; 7: System.Numerics => 0x132b30dd => 8
	i32 388313361, ; 8: Xamarin.Android.Support.Animated.Vector.Drawable => 0x17253111 => 20
	i32 389971796, ; 9: Xamarin.Android.Support.Core.UI => 0x173e7f54 => 26
	i32 442521989, ; 10: Xamarin.Essentials => 0x1a605985 => 48
	i32 465846621, ; 11: mscorlib => 0x1bc4415d => 3
	i32 469710990, ; 12: System.dll => 0x1bff388e => 6
	i32 514659665, ; 13: Xamarin.Android.Support.Compat => 0x1ead1551 => 24
	i32 524864063, ; 14: Xamarin.Android.Support.Print => 0x1f48ca3f => 38
	i32 526420162, ; 15: System.Transactions.dll => 0x1f6088c2 => 52
	i32 539750087, ; 16: Xamarin.Android.Support.Design => 0x202beec7 => 31
	i32 571524804, ; 17: Xamarin.Android.Support.v7.RecyclerView => 0x2210c6c4 => 44
	i32 605376203, ; 18: System.IO.Compression.FileSystem => 0x24154ecb => 55
	i32 690569205, ; 19: System.Xml.Linq.dll => 0x29293ff5 => 60
	i32 692692150, ; 20: Xamarin.Android.Support.Annotations => 0x2949a4b6 => 21
	i32 775507847, ; 21: System.IO.Compression => 0x2e394f87 => 54
	i32 801787702, ; 22: Xamarin.Android.Support.Interpolator => 0x2fca4f36 => 35
	i32 809851609, ; 23: System.Drawing.Common.dll => 0x30455ad9 => 49
	i32 916714535, ; 24: Xamarin.Android.Support.Print.dll => 0x36a3f427 => 38
	i32 955402788, ; 25: Newtonsoft.Json => 0x38f24a24 => 4
	i32 987342438, ; 26: Xamarin.Android.Arch.Lifecycle.LiveData.dll => 0x3ad9a666 => 17
	i32 1098167829, ; 27: Xamarin.Android.Arch.Lifecycle.ViewModel => 0x4174b615 => 19
	i32 1098259244, ; 28: System => 0x41761b2c => 6
	i32 1292763917, ; 29: Xamarin.Android.Support.CursorAdapter.dll => 0x4d0e030d => 28
	i32 1297413738, ; 30: Xamarin.Android.Arch.Lifecycle.LiveData.Core => 0x4d54f66a => 16
	i32 1359785034, ; 31: Xamarin.Android.Support.Design.dll => 0x510cac4a => 31
	i32 1365406463, ; 32: System.ServiceModel.Internals.dll => 0x516272ff => 58
	i32 1373430943, ; 33: ULibrary => 0x51dce49f => 12
	i32 1445445088, ; 34: Xamarin.Android.Support.Fragment => 0x5627bde0 => 34
	i32 1462112819, ; 35: System.IO.Compression.dll => 0x57261233 => 54
	i32 1574652163, ; 36: Xamarin.Android.Support.Core.Utils.dll => 0x5ddb4903 => 27
	i32 1587447679, ; 37: Xamarin.Android.Arch.Core.Common.dll => 0x5e9e877f => 13
	i32 1592978981, ; 38: System.Runtime.Serialization.dll => 0x5ef2ee25 => 57
	i32 1639515021, ; 39: System.Net.Http.dll => 0x61b9038d => 7
	i32 1649992332, ; 40: Xamarin_Scanner_example => 0x6258e28c => 0
	i32 1657153582, ; 41: System.Runtime => 0x62c6282e => 10
	i32 1662014763, ; 42: Xamarin.Android.Support.Vector.Drawable => 0x6310552b => 45
	i32 1776026572, ; 43: System.Core.dll => 0x69dc03cc => 5
	i32 1825098805, ; 44: ULibrary.dll => 0x6cc8cc35 => 12
	i32 1866717392, ; 45: Xamarin.Android.Support.Interpolator.dll => 0x6f43d8d0 => 35
	i32 1867746548, ; 46: Xamarin.Essentials.dll => 0x6f538cf4 => 48
	i32 1877418711, ; 47: Xamarin.Android.Support.v7.RecyclerView.dll => 0x6fe722d7 => 44
	i32 1906670853, ; 48: Xamarin_Scanner_example.dll => 0x71a57d05 => 0
	i32 1916660109, ; 49: Xamarin.Android.Arch.Lifecycle.ViewModel.dll => 0x723de98d => 19
	i32 2037417872, ; 50: Xamarin.Android.Support.ViewPager => 0x79708790 => 47
	i32 2044222327, ; 51: Xamarin.Android.Support.Loader => 0x79d85b77 => 36
	i32 2079903147, ; 52: System.Runtime.dll => 0x7bf8cdab => 10
	i32 2090596640, ; 53: System.Numerics.Vectors => 0x7c9bf920 => 9
	i32 2139458754, ; 54: Xamarin.Android.Support.DrawerLayout => 0x7f858cc2 => 33
	i32 2166116741, ; 55: Xamarin.Android.Support.Core.Utils => 0x811c5185 => 27
	i32 2196165013, ; 56: Xamarin.Android.Support.VersionedParcelable => 0x82e6d195 => 46
	i32 2201231467, ; 57: System.Net.Http => 0x8334206b => 7
	i32 2330457430, ; 58: Xamarin.Android.Support.Core.UI.dll => 0x8ae7f556 => 26
	i32 2330986192, ; 59: Xamarin.Android.Support.SlidingPaneLayout.dll => 0x8af006d0 => 39
	i32 2373288475, ; 60: Xamarin.Android.Support.Fragment.dll => 0x8d75821b => 34
	i32 2440966767, ; 61: Xamarin.Android.Support.Loader.dll => 0x917e326f => 36
	i32 2471841756, ; 62: netstandard.dll => 0x93554fdc => 50
	i32 2475788418, ; 63: Java.Interop.dll => 0x93918882 => 1
	i32 2487632829, ; 64: Xamarin.Android.Support.DocumentFile => 0x944643bd => 32
	i32 2501346920, ; 65: System.Data.DataSetExtensions => 0x95178668 => 53
	i32 2698266930, ; 66: Xamarin.Android.Arch.Lifecycle.LiveData => 0xa0d44932 => 17
	i32 2751899777, ; 67: Xamarin.Android.Support.Collections => 0xa406a881 => 23
	i32 2782647110, ; 68: Xamarin.Android.Support.CustomTabs.dll => 0xa5dbd346 => 29
	i32 2788639665, ; 69: Xamarin.Android.Support.LocalBroadcastManager => 0xa63743b1 => 37
	i32 2788775637, ; 70: Xamarin.Android.Support.SwipeRefreshLayout.dll => 0xa63956d5 => 40
	i32 2819470561, ; 71: System.Xml.dll => 0xa80db4e1 => 11
	i32 2876493027, ; 72: Xamarin.Android.Support.SwipeRefreshLayout => 0xab73cce3 => 40
	i32 2893257763, ; 73: Xamarin.Android.Arch.Core.Runtime.dll => 0xac739c23 => 14
	i32 2903344695, ; 74: System.ComponentModel.Composition => 0xad0d8637 => 56
	i32 2905242038, ; 75: mscorlib.dll => 0xad2a79b6 => 3
	i32 2919462931, ; 76: System.Numerics.Vectors.dll => 0xae037813 => 9
	i32 2921692953, ; 77: Xamarin.Android.Support.CustomView.dll => 0xae257f19 => 30
	i32 2922925221, ; 78: Xamarin.Android.Support.Vector.Drawable.dll => 0xae384ca5 => 45
	i32 3056250942, ; 79: Xamarin.Android.Support.AsyncLayoutInflater.dll => 0xb62ab03e => 22
	i32 3068715062, ; 80: Xamarin.Android.Arch.Lifecycle.Common => 0xb6e8e036 => 15
	i32 3111772706, ; 81: System.Runtime.Serialization => 0xb979e222 => 57
	i32 3191408315, ; 82: Xamarin.Android.Support.CustomTabs => 0xbe3906bb => 29
	i32 3204380047, ; 83: System.Data.dll => 0xbefef58f => 51
	i32 3204912593, ; 84: Xamarin.Android.Support.AsyncLayoutInflater => 0xbf0715d1 => 22
	i32 3233339011, ; 85: Xamarin.Android.Arch.Lifecycle.LiveData.Core.dll => 0xc0b8d683 => 16
	i32 3247949154, ; 86: Mono.Security => 0xc197c562 => 61
	i32 3296380511, ; 87: Xamarin.Android.Support.Collections.dll => 0xc47ac65f => 23
	i32 3317144872, ; 88: System.Data => 0xc5b79d28 => 51
	i32 3321585405, ; 89: Xamarin.Android.Support.DocumentFile.dll => 0xc5fb5efd => 32
	i32 3352662376, ; 90: Xamarin.Android.Support.CoordinaterLayout => 0xc7d59168 => 25
	i32 3357663996, ; 91: Xamarin.Android.Support.CursorAdapter => 0xc821e2fc => 28
	i32 3366347497, ; 92: Java.Interop => 0xc8a662e9 => 1
	i32 3404865022, ; 93: System.ServiceModel.Internals => 0xcaf21dfe => 58
	i32 3429136800, ; 94: System.Xml => 0xcc6479a0 => 11
	i32 3430777524, ; 95: netstandard => 0xcc7d82b4 => 50
	i32 3439690031, ; 96: Xamarin.Android.Support.Annotations.dll => 0xcd05812f => 21
	i32 3476120550, ; 97: Mono.Android => 0xcf3163e6 => 2
	i32 3486566296, ; 98: System.Transactions => 0xcfd0c798 => 52
	i32 3498942916, ; 99: Xamarin.Android.Support.v7.CardView.dll => 0xd08da1c4 => 43
	i32 3509114376, ; 100: System.Xml.Linq => 0xd128d608 => 60
	i32 3547625832, ; 101: Xamarin.Android.Support.SlidingPaneLayout => 0xd3747968 => 39
	i32 3567349600, ; 102: System.ComponentModel.Composition.dll => 0xd4a16f60 => 56
	i32 3664423555, ; 103: Xamarin.Android.Support.ViewPager.dll => 0xda6aaa83 => 47
	i32 3672681054, ; 104: Mono.Android.dll => 0xdae8aa5e => 2
	i32 3676310014, ; 105: System.Web.Services.dll => 0xdb2009fe => 59
	i32 3678221644, ; 106: Xamarin.Android.Support.v7.AppCompat => 0xdb3d354c => 42
	i32 3681174138, ; 107: Xamarin.Android.Arch.Lifecycle.Common.dll => 0xdb6a427a => 15
	i32 3689375977, ; 108: System.Drawing.Common => 0xdbe768e9 => 49
	i32 3714038699, ; 109: Xamarin.Android.Support.LocalBroadcastManager.dll => 0xdd5fbbab => 37
	i32 3718463572, ; 110: Xamarin.Android.Support.Animated.Vector.Drawable.dll => 0xdda34054 => 20
	i32 3776062606, ; 111: Xamarin.Android.Support.DrawerLayout.dll => 0xe112248e => 33
	i32 3829621856, ; 112: System.Numerics.dll => 0xe4436460 => 8
	i32 3832731800, ; 113: Xamarin.Android.Support.CoordinaterLayout.dll => 0xe472d898 => 25
	i32 3862817207, ; 114: Xamarin.Android.Arch.Lifecycle.Runtime.dll => 0xe63de9b7 => 18
	i32 3874897629, ; 115: Xamarin.Android.Arch.Lifecycle.Runtime => 0xe6f63edd => 18
	i32 3883175360, ; 116: Xamarin.Android.Support.v7.AppCompat.dll => 0xe7748dc0 => 42
	i32 3920810846, ; 117: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 55
	i32 3945713374, ; 118: System.Data.DataSetExtensions.dll => 0xeb2ecede => 53
	i32 4063435586, ; 119: Xamarin.Android.Support.CustomView => 0xf2331b42 => 30
	i32 4105002889, ; 120: Mono.Security.dll => 0xf4ad5f89 => 61
	i32 4151237749, ; 121: System.Core => 0xf76edc75 => 5
	i32 4216993138, ; 122: Xamarin.Android.Support.Transition.dll => 0xfb5a3572 => 41
	i32 4219003402 ; 123: Xamarin.Android.Support.v7.CardView => 0xfb78e20a => 43
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [124 x i32] [
	i32 4, i32 46, i32 13, i32 24, i32 14, i32 59, i32 41, i32 8, ; 0..7
	i32 20, i32 26, i32 48, i32 3, i32 6, i32 24, i32 38, i32 52, ; 8..15
	i32 31, i32 44, i32 55, i32 60, i32 21, i32 54, i32 35, i32 49, ; 16..23
	i32 38, i32 4, i32 17, i32 19, i32 6, i32 28, i32 16, i32 31, ; 24..31
	i32 58, i32 12, i32 34, i32 54, i32 27, i32 13, i32 57, i32 7, ; 32..39
	i32 0, i32 10, i32 45, i32 5, i32 12, i32 35, i32 48, i32 44, ; 40..47
	i32 0, i32 19, i32 47, i32 36, i32 10, i32 9, i32 33, i32 27, ; 48..55
	i32 46, i32 7, i32 26, i32 39, i32 34, i32 36, i32 50, i32 1, ; 56..63
	i32 32, i32 53, i32 17, i32 23, i32 29, i32 37, i32 40, i32 11, ; 64..71
	i32 40, i32 14, i32 56, i32 3, i32 9, i32 30, i32 45, i32 22, ; 72..79
	i32 15, i32 57, i32 29, i32 51, i32 22, i32 16, i32 61, i32 23, ; 80..87
	i32 51, i32 32, i32 25, i32 28, i32 1, i32 58, i32 11, i32 50, ; 88..95
	i32 21, i32 2, i32 52, i32 43, i32 60, i32 39, i32 56, i32 47, ; 96..103
	i32 2, i32 59, i32 42, i32 15, i32 49, i32 37, i32 20, i32 33, ; 104..111
	i32 8, i32 25, i32 18, i32 18, i32 42, i32 55, i32 53, i32 30, ; 112..119
	i32 61, i32 5, i32 41, i32 43 ; 120..123
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 4; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 4

; Function attributes: "frame-pointer"="none" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 4
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 4
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="none" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" "stackrealign" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="none" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" "stackrealign" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"NumRegisterParameters", i32 0}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}

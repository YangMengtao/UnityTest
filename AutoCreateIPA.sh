#!/bin/bash
#set -x

# 	自动打过流程
# 	1.自动生成xcode工程
#	2.clean xcode工程
#	3.编译xcode工程	
#	4.生成ipa

CurrentPath=$(pwd)
OutPutPath=$CurrentPath"/IOS"
SDK_Str="iphoneos"
SDK_Ver="9.2"
SDK=$SDK_Str$SDK_Ver
Version="666"
SvnVer="233"
ProjectName="igame"
AppPath="/build/iPhone/build/Release-iphoneos/mmo.app"

# 输入参数
function InputParam()
{
	read -p "请输入输出路径(默认为./IOS):" OutPutPath

	if [ "$OutPutPath" == "" ]
	then
		OutPutPath=$CurrentPath"/IOS"
	fi

	read -p "请输入SDK版本(如8.0):" SDK_Ver

	if [ "$SDK_Ver" == "" ]
	then
		SDK_Ver="9.0"
	fi

	if [ ! -d $OutPutPath"/release" ]
	then
		mkdir -p $OutPutPath"/release"
	fi

	SDK=$SDK_Str$SDK_Ver
}

# 创建xcode工程
function CreateProject()
{
	/Applications/Unity/Unity.app/Contents/MacOS/Unity -projectPath $CurrentPath -executeMethod PerformBuild.CommandLineBuild -buildOSXPlayer -quit

	/Applications/Unity/Unity.app/Contents/MacOS/Unity -quit -projectPath /Users/ymt/Documents/work/Y/ -executeMethod BuildManager.BuildIOS 

	mv $CurrentPath/Assets/build $OutPutPath
}

# 修改plist文件
function EditPlist()
{
	cd $OutPutPath/build/iPhone/

	sed -i "" "s/<string>autobuild_version<\/string>/<string>${Version}<\/string>/" $OutPutPath/build/iPhone/Info.plist
	sed -i "" "s/<string>autobuild_build<\/string>/<string>${SvnVer}<\/string>/" $OutPutPath/build/iPhone/Info.plist
}

# 清理xcode工程
function Clean()
{
	xcodebuild -alltargets clean -sdk $SDK -configuration Release
}

# 编译xcode工程
function Build()
{
	BUILD_OPT_MAKE_DSYM="GCC_GENERATE_DEBUGGING_SYMBOLS=YES DEBUG_INFORMATION_FORMAT=dwarf DEPLOYMENT_POSTPROCESSING=YES STRIP_INSTALLED_PRODUCT=YES SEPARATE_STRIP=YES COPY_PHASE_STRIP=NO"

	xcodebuild -sdk $SDK -configuration Release $BUILD_OPT_MAKE_DSYM
	if test $? -eq 0
	then
		buildDate=`date -v -0d +%Y%m%d%H%M`

		rm -rf $OutPutPath/build/iPhone/build/monkeykingtd.app/Data
		cp -rf $OutPutPath/build/iPhone/Data $OutPutPath/build/iPhone/build/monkeykingtd.app/Data
		if [ "$PLATFORM_NAME" == "iphonesimulator" ]
		then 
			cp -f $OutPutPath/build/iPhone/*.png $OutPutPath/build/iPhone/build/monkeykingtd.app/                      
		fi

		
		/usr/bin/xcrun -sdk SDK PackageApplication -v $OutPutPath/build/iPhone/build/monkeykingtd.app -o $OutPutPath/release/$ProjectName/$ipaName

		say -v Ting-Ting 骚年，还等啥呢
	else
		say -v Ting-Ting 骚年，没build成功。咋地了。
	fi
}

# 创建iap
function CreateIAP()
{
	read -p "请输入*.app的路径(如:/build/iPhone/build/Release-iphoneos/mmo.app):" AppPath

	buildDate=`date -v -0d +%Y%m%d%H%M`
	ipaName=$ProjectName"_"${SvnVer}"_"${buildDate}".ipa"

	/usr/bin/xcrun -sdk SDK PackageApplication -v $OutPutPath$AppPath -o $OutPutPath/release/$ipaName
}

InputParam()
CreateProject()
EditPlist()
Clean()
Build()
CreateIAP()

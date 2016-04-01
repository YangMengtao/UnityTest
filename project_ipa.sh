project_name="igame"
Path=$(pwd)
buildPath=$Path"/ios"
echo "当前路径:"$Path
echo "输出路径:"$buildPath

:<<eof
if [ ! -d $buildPath ]
then
	mkdir -p $buildPath
	mkdir -p $buildPath"/build"
fi

#read -p "请输入提交svn注释：" miss

#read -p "请输入版本号：" version
version=666

ConfigurationName="ConfigurationTest"
#echo "ConfigurationTest"

#read -p "请输入需要构建版本的配置文件的名字：" ConfigurationName
if [ "$ConfigurationName" = "" ]
then
	ConfigurationName="ConfigurationTest"
fi

rm -rf $Path/build

#cd /Users/Freezen/Documents/TrexBuild/MKAS/trunk/
#svn up
#svn_ver=`svn up | tail -n 1 | grep -Eo "[0-9]+"`
svn_ver="2333"

/Applications/Unity/Unity.app/Contents/MacOS/Unity -projectPath $Path -executeMethod PerformBuild.CommandLineBuild -buildOSXPlayer -quit

mv $Path/Assets/build "./ios"
eof

#:<<eof
#rm -rf $buildPath/build/iPhone/Unity-iPhone/Images.xcassets/AppIcon.appiconset
#cp -rf $buildPath/build/$project_name/* $buildPath/build/iPhone/

cd $buildPath/build/iPhone/

:<<eof
cp -rf $buildPath"/build/iPhone/Data/Raw/"$ConfigurationName".txt" /tmp/
rm -rf $buildPath/build/iPhone/Data/Raw/Configuration*
cp /tmp/$ConfigurationName".txt" $buildPath/build/iPhone/Data/Raw/Configuration.txt
sed -i "" "s/SED_VERSION_MKAS/${version}/" $buildPath/build/iPhone/Data/Raw/Configuration.txt
eof

#xcodebuild clean -sdk iphoneos8.0 -configuration Release
#xcodebuild -sdk iphoneos8.0 -configuration Release

sed -i "" "s/<string>autobuild_version<\/string>/<string>${version}<\/string>/" $buildPath/build/iPhone/Info.plist
sed -i "" "s/<string>autobuild_build<\/string>/<string>${svn_ver}<\/string>/" $buildPath/build/iPhone/Info.plist

xcodebuild -target Unity-iPhone clean -sdk iphoneos7.0 -configuration Release
BUILD_OPT_MAKE_DSYM="GCC_GENERATE_DEBUGGING_SYMBOLS=YES DEBUG_INFORMATION_FORMAT=dwarf DEPLOYMENT_POSTPROCESSING=YES STRIP_INSTALLED_PRODUCT=YES SEPARATE_STRIP=YES COPY_PHASE_STRIP=NO"

xcodebuild -sdk iphoneos7.0 -configuration Release $BUILD_OPT_MAKE_DSYM
if test $? -eq 0
then
	builddate=`date -v -0d +%Y%m%d%H%M`

	rm -rf $buildPath/build/iPhone/build/monkeykingtd.app/Data
	cp -rf $buildPath/build/iPhone/Data $buildPath/build/iPhone/build/monkeykingtd.app/Data
	if [ "$PLATFORM_NAME" == "iphonesimulator" ]
	then 
		cp -f $buildPath/build/iPhone/*.png $buildPath/build/iPhone/build/monkeykingtd.app/                      
	fi

	ipaname=$project_name"_"${builddate}".ipa"
	/usr/bin/xcrun -sdk iphoneos7.1 PackageApplication -v $buildPath/build/iPhone/build/monkeykingtd.app -o $buildPath/release/$project_name/$ipaname

#	cd /Users/Freezen/Documents/TrexBuild/MKAS/trunk/release/$project_name/
#	svn add *
#	svn commit -m "\"$miss"\"

	say -v Ting-Ting 骚年，还等啥呢
else
	say -v Ting-Ting 骚年，没build成功。咋地了。
fi

#eof


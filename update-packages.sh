for PROJECT in $(find . -iname '*.csproj')
do
  for PACKAGE in $(dotnet list $PROJECT package --outdated | grep '>' | sed -E 's/^ *> ([a-zA-Z0-9\.]*) .*$/\1/g')
  do
    dotnet add $PROJECT package $PACKAGE
  done
done
echo "Update Complete"

param(
    [Parameter(Mandatory=$true)]
    [string]$environment
)

$region = "eu-central-1"
$bucketName = "{0}-deployment-54534" -f $environment
$stackName = "{0}-sqs" -f $environment
Write-Output "StackName: "+ $stackName

# Build and package the Lambda function code
Remove-Item -Path .\publish -Recurse
dotnet publish --configuration "Release" --output ./publish --framework "net6.0" /p:GenerateRuntimeConfigurationFiles=true --runtime linux-x64 --self-contained False 
cd ./publish

$zipArchiveName = "sqs-function-"+[guid]::NewGuid().ToString()+".zip";

Compress-Archive -Path * -DestinationPath $zipArchiveName

# Upload the code to S3
aws s3 cp $zipArchiveName s3://$bucketName/

cd..

# Update SAM template with the S3 URI
$codeUri = 's3://{0}/{1}' -f $bucketName, $zipArchiveName
$templatePath = "./serverless.template"
(Get-Content $templatePath) -replace '"CodeUri":\s*".*?"', ('"CodeUri": "{0}"' -f $codeUri) | Set-Content $templatePath

# Deploy CloudFormation stack
aws cloudformation deploy --template-file $templatePath --stack-name $stackName --parameter-overrides EnvironmentName=$environment --region $region --capabilities CAPABILITY_IAM
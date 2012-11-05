require 'rubygems'
require 'albacore'
require 'win32api'
require 'net/http'
require 'version_bumper'

desc "Builder for Exceptional"

task :default => [:package]

SOURCE_ROOT       = 'src/'
APPLICATION_NAME  = 'Exceptional'
SOLUTION_FILE     = File.join(SOURCE_ROOT, 'Exceptional.sln')
COMPANY           = "mroach"
COPYRIGHT         = "#{COMPANY} 2012"
BUILD_CONFIG      = "Release"
VERSION_FILE      = 'VERSION'

BUILD_PROPERTIES = {
  :configuration => BUILD_CONFIG,
  :nowarn => "1573;1572;1591;1574" # suppresses XML comment warnings which we don't care about
}

def current_build_number
  bumper_version.to_s
end

# Find the current git branch name
def current_branch_name
  ref = %x[git symbolic-ref -q HEAD]
  ref.split('/').reverse.first.chomp
end

def current_git_hash
  %x[git rev-parse HEAD]
end

# If the current git branch is not 'master' then we want to append
# the branch name to the application name. This prevents accidental
# overwriting of production code with branches in test environments
def application_build_name
  name = APPLICATION_NAME
  branch_name = current_branch_name
  
  if branch_name != 'master'
    name += '-' + branch_name
  end
  
  name
end

def application_package_version
  version = current_build_number
  branch_name = current_branch_name
  
  if branch_name != 'master'
    version += '-' + branch_name
  end
  
  version
end

assemblyinfo :assemblyinfo  do |asm|
  asm.version = current_build_number
  asm.company_name = COMPANY
  asm.product_name = APPLICATION_NAME
  asm.copyright = COPYRIGHT
  asm.title = application_build_name
  asm.description = application_build_name

  ["Exceptional.Core", "Exceptional.Web.Mvc"].each do |proj|
    asm.output_file = File.join(SOURCE_ROOT, proj, "Properties/AssemblyInfo.cs")
    asm.execute
  end
end

exec :fetch_packages do |cmd|
  cmd.command = "nuget"
  cmd.parameters = "install src\\Exceptional.Core\\packages.config -o src\\packages"
end

msbuild :build => [:assemblyinfo] do |msb|
  msb.targets :clean, :build
  msb.solution = SOLUTION_FILE
  msb.properties = BUILD_PROPERTIES
  msb.verbosity = "minimal"
end

nugetpack :nugetpack do |nuget|
  project_dir = File.join(SOURCE_ROOT, "Exceptional.Web.Mvc")
  nuget.command = "tools/nuget/nuget.exe"
  nuget.nuspec = File.join(project_dir, "Exceptional.Web.Mvc.nuspec")
  nuget.base_folder = project_dir
  nuget.output = "output/"
end

#task :copy do
#  Dir.glob(File.join(SOURCE_ROOT, "Exceptional.Web.Mvc/bin/", BUILD_CONFIG, "Exceptional*.dll")) do |f|
#    cp(f, "../lib/")
#  end
#end
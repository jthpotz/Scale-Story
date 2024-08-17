import os
import argparse

def CreateFolder(name):
    path = './../' + name
    if not os.path.exists(path):
        os.mkdir(path)

def CreateBasicFile(name, fileType):
    createdFilePath = './../' + name + '/' + name + fileType + '.cs'
    bpFilePath = './bp' + fileType + '.txt'

    try:
        createdFile = open(createdFilePath, 'x')
    except FileExistsError as e:
        print("File already exists for " + fileType + ".", e)
        exit()

    bpFile = open(bpFilePath)

    contents = bpFile.read()
    modified = contents.replace('###PLACEHOLDER###', name)
    createdFile.write(modified)

def CreateEnumFolder(name, enumName):
    path = './../' + name + '/' + enumName
    if not os.path.exists(path):
        os.mkdir(path)

def CreateEnumFile(name, enumName, enumFolderName, enumFileName):
    createdFilePath = './../' + name + '/' + enumFolderName + '/' + enumFileName + 'Enum.cs'
    bpFilePath = './bpBaseEnum.txt'

    try:
        createdFile = open(createdFilePath, 'x')
    except FileExistsError as e:
        print("File already exists for " + enumName + ".", e)
        exit()

    bpFile = open(bpFilePath)

    contents = bpFile.read()
    modified = contents.replace('###PLACEHOLDER1###', name + '.' + enumFolderName)
    modified = modified.replace('###PLACEHOLDER2###', enumName)
    createdFile.write(modified)

def CreatePartialEnumFile(name, enumName, enumFolderName, enumAbbreviation, enumPartialName):
    createdFilePath = './../' + name + '/' + enumFolderName + '/' + enumAbbreviation + '_' + enumPartialName + '.cs'
    bpFilePath = './bpPartialEnum.txt'

    try:
        createdFile = open(createdFilePath, 'x')
    except FileExistsError as e:
        print("File already exists for " + enumAbbreviation + '_' + enumPartialName + ".", e)
        exit()

    bpFile = open(bpFilePath)

    contents = bpFile.read()
    modified = contents.replace('###PLACEHOLDER1###', name + '.' + enumFolderName)
    modified = modified.replace('###PLACEHOLDER2###', enumName)
    modified = modified.replace('###PLACEHOLDER3###', enumPartialName)
    createdFile.write(modified)

def CreateAllBasicFiles(name):
    CreateBasicFile(name, 'Data')
    CreateBasicFile(name, 'State')
    CreateBasicFile(name, 'Visual')
    CreateBasicFile(name, 'Factory')
    CreateBasicFile(name, 'DataLoader')
    CreateBasicFile(name, 'Computations')
    
if __name__ == "__main__":
    parser = argparse.ArgumentParser(prog="BoilerPlateGenerator", description="Generate boilerplate for various files")
    parser.add_argument('name', help='Name for the files being created.')
    parser.add_argument('-b', '--basic', action='store_true', help='Create boilerplate for all basic files: Data, State, Visual, Factory, DataLoader, and Computations.')
    parser.add_argument('-d', '--data', action='store_true', help='Create boilerplate for Data.')
    parser.add_argument('-s', '--state', action='store_true', help='Create boilerplate for State.')
    parser.add_argument('-v', '--visual', action='store_true', help='Create boilerplate for Visual.')
    parser.add_argument('-f', '--factory', action='store_true', help='Create boilerplate for Factory.')
    parser.add_argument('-l', '--loader', action='store_true', help='Create boilerplate for DataLoader.')
    parser.add_argument('-c', '--computations', action='store_true', help='Create boilerplate for Computations.')
    parser.add_argument('-e', '--enum', nargs=3, metavar=('enumName', 'enumFolderName', 'enumFileName'), help='Create boilerplate for a base enum. (Only if -p is not set)')
    parser.add_argument('-p', '--partial', nargs=2, metavar=('enumAbbreviation', 'enumPartialName'), help='Create boilerplate for partial enum. (Requires -e to be set)')
    args = parser.parse_args()

    CreateFolder(args.name)

    if args.basic:
        CreateAllBasicFiles(args.name)
    if args.data:
        CreateBasicFile(args.name, 'Data')
    if args.state:
        CreateBasicFile(args.name, 'State')
    if args.visual:
        CreateBasicFile(args.name, 'Visual')
    if args.factory:
        CreateBasicFile(args.name, 'Factory')
    if args.loader:
        CreateBasicFile(args.name, 'DataLoader')
    if args.computations:
        CreateBasicFile(args.name, 'Computations')
    if args.enum:
        CreateEnumFolder(args.name, args.enum[1])
        if not args.partial:
            CreateEnumFile(args.name, args.enum[0], args.enum[1], args.enum[2])
        if args.partial:
            CreatePartialEnumFile(args.name, args.enum[0], args.enum[1], args.partial[0], args.partial[1])
    elif args.partial:
        print("-p set but -e is not. To use -p, set -e as well.")
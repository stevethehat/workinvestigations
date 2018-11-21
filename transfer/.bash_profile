export PATH=/usr/local/mysql/bin:$PATH
export PATH=/scripts:$PATH

export PS1="\[\033[36m\]\u\[\033[m\]@\[\033[32m\]\h:\[\033[33;1m\]\w\[\033[m\]\$ "
export CLICOLOR=1
export LSCOLORS=ExFxBxDxCxegedabagacad
export MONO_MANAGED_WATCHER=false


alias ls='ls -lha'
alias grepr="grep . -HEre"
alias screen=/Users/stevework/bin/screen
alias numfiles="ls -1 | wc -l"
alias lszip='unzip -l'
alias chrome='/Applications/Google\ Chrome.app/Contents/MacOS/Google\ Chrome'
alias chromedebug='/Applications/Google\ Chrome.app/Contents/MacOS/Google\ Chrome'
alias code='/Applications/Visual\ Studio\ Code.app/Contents/MacOS/Electron'

alias rg='rg -p'
alias more='more -R'

pyclean () {
        find . -type f -name "*.py[co]" -delete
        find . -type d -name "__pycache__" -delete
}

killscreens () {
    screen -ls | grep Detached | cut -d. -f1 | awk '{print $1}' | xargs kill
}

function swap()
{
  tmpfile=$(mktemp $(dirname "$1")/XXXXXX)
  mv "$1" "$tmpfile" && mv "$2" "$1" &&  mv "$tmpfile" "$2"
}

function tab_title {
  echo -n -e "\033]0;${PWD##*/}\007"
}

PROMPT_COMMAND="tab_title ; $PROMPT_COMMAND"

function up()
{
    dir=""
    if [ -z "$1" ]; then
        dir=..
    elif [[ $1 =~ ^[0-9]+$ ]]; then
        x=0
        while [ $x -lt ${1:-1} ]; do
            dir=${dir}../
            x=$(($x+1))
        done
    else
        dir=${PWD%/$1/*}/$1
    fi
    cd "$dir";
}

function upstr()
{
    echo "$(up "$1" && pwd)";
}

export PATH="$HOME/.fastlane/bin:$PATH"

parse_git_branch() {
   git branch 2> /dev/null | sed -e '/^[^*]/d' -e 's/* \(.*\)/ (\1)/'
}


PS1="\u:\[\033[01;33m\]\w \[\033[01;33m\]:\[\033[0;33m\]\$(parse_git_branch) \[\033[0;160m\]\[\033[0;m\]\$ "

export PATH="$HOME/.fastlane/bin:$PATH"

alias showFiles='defaults write com.apple.finder AppleShowAllFiles YES; killall Finder /System/Library/CoreServices/Finder.app'
alias hideFiles='defaults write com.apple.finder AppleShowAllFiles NO; killall Finder /System/Library/CoreServices/Finder.app'

test -e "${HOME}/.iterm2_shell_integration.bash" && source "${HOME}/.iterm2_shell_integration.bash"


using Ink.Runtime;
using Spectre.Console;

namespace ConsoleRPG.Ink
{
    internal class InkStorytelling
    {
        public Story CurrentStory { get; set; }

        public void SetCurrentStory(string inkFilePath)
        {
            string inkJson = File.ReadAllText(inkFilePath);

            CurrentStory = new Story(inkJson);
        }

        public void RandomEncounter()
        {

        }

        public void ChangePlayerNameVariable(Story story, string playerName)
        {
            story.variablesState["playerName"] = playerName;
        }

        private void ParseTags()
        {
            List<string> tags = CurrentStory.currentTags;

            foreach (string tag in tags)
            {
                string prefix = tag.Split(' ')[0];
                string param = tag.Split(' ')[1];
                switch (prefix.ToLower())
                {
                    case "anim":
                        //string animation = tag.Split(' ')[2];
                        //StartAnimation(param, animation);
                        break;
                    case "speaker":
                        //ChangeSpeaker(param);
                        break;
                    case "show":
                        AnsiConsole.Markup($"Тег спарсился {param}");
                        //string position = tag.Split(' ')[2];
                        //ShowCharacter(param, position);
                        break;
                    case "hide":
                        //HideCharacter(param);
                        break;
                    case "music":
                        //audioManager.PlayMusic(musicData.clips[musicData.clips.FindIndex(x => x.name == param)]);
                        break;
                    case "sound":
                        //audioManager.PlaySound(soundData.clips[soundData.clips.FindIndex(x => x.name == param)]);
                        break;
                }
            }
        }

        public void EnterDialogueMode()
        {
            if (CurrentStory.canContinue)
            {
                string currentSentence = CurrentStory.Continue();
                ParseTags();
                ConsoleMessages.Message(currentSentence);

                if (CurrentStory.currentChoices.Count != 0)
                {
                    ChoiceMode();
                }

                EnterDialogueMode();
            }
            else
            {
                ExitDialogueMode();
            }
        }

        public void ChoiceMode()
        {
            Dictionary<int, string> choises = new Dictionary<int, string>();

            for (int i = 0; i < CurrentStory.currentChoices.Count; i++)
            {
                Choice choice = CurrentStory.currentChoices[i];
                choises.Add(i + 1, choice.text);
            }

            var choise = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]Cделайте выбор[/]")
                    .MoreChoicesText("[grey](Пролистайте ниже, чтобы увидеть все доступные варианты)[/]")
                    .AddChoices(choises.Values));

            int choiceIndex = choises.Where(v => v.Value == choise).FirstOrDefault().Key - 1;
            CurrentStory.ChooseChoiceIndex(choiceIndex);
        }

        public void ExitDialogueMode()
        {

        }
    }
}

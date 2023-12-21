import * as icons from '@mui/icons-material'
import stringSimilarity from 'string-similarity'

function useIcons(word, setIconValid) {
  const iconsNames = Object.keys(icons)

  if (iconsNames.includes(word)) {
      const Icon = icons[word]
      setIconValid(true)
      return Icon
  }
  const Icon = icons["MoreHoriz"]
  setIconValid(false)
  return Icon
}
export default useIcons
